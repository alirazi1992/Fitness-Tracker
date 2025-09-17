using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;

namespace FitnessTracker
{
    public class FitnessDb
    {
        private readonly string _connString;

        public FitnessDb(string path)
        {
            _connString = $"Data Source={path}";
        }

        public void EnsureCreated()
        {
            using (var con = new SqliteConnection(_connString))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS workouts(
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        date TEXT NOT NULL,
                        type TEXT NOT NULL,
                        reps INTEGER,
                        duration_min REAL,
                        calories REAL
                      );";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddWorkout(DateTime date, string type, int reps, double duration, double calories)
        {
            using (var con = new SqliteConnection(_connString))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO workouts(date, type, reps, duration_min, calories) VALUES ($d,$t,$r,$dur,$c)";
                    cmd.Parameters.AddWithValue("$d", date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("$t", type);
                    cmd.Parameters.AddWithValue("$r", reps);
                    cmd.Parameters.AddWithValue("$dur", duration);
                    cmd.Parameters.AddWithValue("$c", calories);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetWorkoutsTable(DateTime from, DateTime to)
        {
            var table = new DataTable();
            using (var con = new SqliteConnection(_connString))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM workouts WHERE date >= $f AND date < $t ORDER BY date";
                    cmd.Parameters.AddWithValue("$f", from.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("$t", to.ToString("yyyy-MM-dd"));
                    using (var reader = cmd.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                }
            }
            return table;
        }

        public List<DailyTotal> GetDailyTotals(DateTime from, DateTime to)
        {
            var list = new List<DailyTotal>();
            using (var con = new SqliteConnection(_connString))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"SELECT date, SUM(calories) as cal, SUM(duration_min) as dur
                                        FROM workouts
                                        WHERE date >= $f AND date < $t
                                        GROUP BY date
                                        ORDER BY date";
                    cmd.Parameters.AddWithValue("$f", from.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("$t", to.ToString("yyyy-MM-dd"));
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new DailyTotal
                            {
                                Date = DateTime.Parse(reader.GetString(0)),
                                TotalCalories = reader.IsDBNull(1) ? 0 : reader.GetDouble(1),
                                TotalDurationMin = reader.IsDBNull(2) ? 0 : reader.GetDouble(2)
                            });
                        }
                    }
                }
            }
            return list;
        }
    }

    public class DailyTotal
    {
        public DateTime Date { get; set; }
        public double TotalCalories { get; set; }
        public double TotalDurationMin { get; set; }
    }
}
