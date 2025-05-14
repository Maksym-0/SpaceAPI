using Npgsql;
using SpaceAPI.Models;
using SpaceAPI;
namespace SpaceAPI.Database
{
    public class DBPhoto
    {
        NpgsqlConnection _connection = new NpgsqlConnection(Constants.DBConnect);

        public async Task InsertPhotoAsync(SpacePhoto sp)
        {
            var sql = "INSERT INTO PUBLIC.\"PhotoSearch\" (\"title\", \"date\", \"explanation\", \"url\")"+
                "VALUES (@title, @date, @explanation, @url)";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, _connection);

            cmd.Parameters.AddWithValue("title", sp.title);
            cmd.Parameters.AddWithValue("date", sp.date);
            cmd.Parameters.AddWithValue("explanation", sp.explanation);
            cmd.Parameters.AddWithValue("url", sp.url);

            await _connection.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }
        public async Task<List<SpacePhoto>> ReadPhotoAsync()
        {
            List<SpacePhoto> photolist = new List<SpacePhoto>();
            await _connection.OpenAsync();
            var sql = "SELECT \"title\", \"date\", \"explanation\", \"url\" " +
                "FROM public.\"PhotoSearch\"";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, _connection);
            NpgsqlDataReader npgsqlData = await cmd.ExecuteReaderAsync();
            while(await npgsqlData.ReadAsync())
            {
                SpacePhoto photo = new SpacePhoto
                {
                    title = npgsqlData.GetString(0),
                    date = npgsqlData.GetString(1),
                    explanation = npgsqlData.GetString(2),
                    url = npgsqlData.GetString(3)
                };
                photolist.Add(photo);
            }
            await _connection.CloseAsync();
            return photolist;
        }
        public async Task EditPhotoAsync(string title, string explanation, string date)
        {
            var sql = "UPDATE public.\"PhotoSearch\" " +
                "SET \"title\" = @title, \"explanation\" = @explanation " +
                "WHERE \"date\" = @date";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, _connection);

            cmd.Parameters.AddWithValue("title", title);
            cmd.Parameters.AddWithValue("explanation", explanation);
            cmd.Parameters.AddWithValue("date", date);

            await _connection.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }
        public async Task DeletePhotoAsync(string date)
        {
            var sql = "DELETE FROM public.\"PhotoSearch\" " +
                "WHERE \"date\" = @date";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, _connection);

            cmd.Parameters.AddWithValue("date", date);

            await _connection.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }
    }
}
