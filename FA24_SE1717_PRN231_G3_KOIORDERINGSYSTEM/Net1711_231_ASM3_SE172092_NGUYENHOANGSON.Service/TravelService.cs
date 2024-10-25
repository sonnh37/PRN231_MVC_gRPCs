using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Common;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Data;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Data.Models;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Service.Base;

namespace Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Service
{
    public interface ITravelService
    {
        Task<IBusinessResult> GetAll();
    }
    public class TravelService : ITravelService
    {
        private readonly UnitOfWork _unitOfWork;
        public TravelService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        
        public async Task<IBusinessResult> GetAll()
        {
            #region Business rule

            #endregion
            var travel = await _unitOfWork.Travel.GetAllAsync();
            if (travel == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Travel>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, travel);
            }
        }

        
    }
}
