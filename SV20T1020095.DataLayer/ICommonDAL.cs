using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV20T1020095.DataLayer
{
    public interface ICommonDAL<T> where T : class
    {
        /// <summary>
        /// Tìm kiếm và lấy danh sách dữ liệu dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần hiện thị</param>
        /// <param name="pageSize">số dòng hiện thị trên từng trang (bằng 0 nếu không phân trang)</param>
        /// <param name="searchValue">giá trị cần tìm kiếm(chuỗi rỗng nếu lấy toàn bộ dữ liệu)</param>
        /// <returns></returns>
        IList<T> List(int page = 1, int pageSize = 0, string searchValue = "");
        /// <summary>
        /// đếm số dòng dữ liệu tìm được
        /// </summary>
        /// <param name="searchValue">giá trị cần tìm kiếm (chuỗi rộng nếu lấy toàn bộ dữ liệu)</param>
        /// <returns></returns>
        int Count(string searchValue = "");
        /// <summary>
        /// bổ sung dữ liệu vào cơ sở dữ liệu. Hàm trả về ID của dữ liệu được bổ sung 
        /// (trả về 0 nếu việc bổ sung không thành công)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(T data);
        /// <summary>
        /// câph nhập dữ liệu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(T data);
        /// <summary>
        /// xoá dữ liệu dữ trên id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// lấy một bảng ghi dựa vào id (trả về null nếu dữ liệu không tồn tại)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T? Get(int id);
        /// <summary>
        /// kiểm tra bản ghi dữ liệu có id hiện đang được sử dụng bởi dữ liệu khác hay không
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IsUsed(int id);
    }
}
