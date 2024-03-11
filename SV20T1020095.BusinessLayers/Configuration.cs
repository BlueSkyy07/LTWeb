using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV20T1020095.BusinessLayers
{
    /// <summary>
    /// KHởi tạo, lưu trữ các thông tn cấu hình của BussinessLayer
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// Chuỗi kết thông số kết nối đến CSDL
        /// </summary>
        public static string ConnectionString { get; private set; } = "";
        /// <summary>
        /// Khỏi tạo cấu hình cho BussinessLayer
        /// (Hmaf này phải được gọi trước khi ứng dụng chạy)
        /// </summary>
        /// <param name="connectionString"></param>
        public static void Initialize(string connectionString)
        {
            Configuration.ConnectionString = connectionString;
        }
    }
}
//static class là gì và khác với class thông thường  ch-ỗ nào