using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Common;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Data;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Data.Models;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Service.Base;

namespace Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Service;

public interface ITravelService
{
    Task<IBusinessResult> GetAll();
    Task<IBusinessResult> GetById(Guid code);
    Task<IBusinessResult> Create(Travel currency);
    Task<IBusinessResult> Update(Travel currency);
    Task<IBusinessResult> Save(Travel currency);
    Task<IBusinessResult> DeleteById(Guid code);
}

public class TravelService : ITravelService
{
    private readonly UnitOfWork _unitOfWork;

    public TravelService()
    {
        _unitOfWork ??= new UnitOfWork();
    }

    public Task<IBusinessResult> Create(Travel currency)
    {
        throw new NotImplementedException();
    }

    public async Task<IBusinessResult> DeleteById(Guid code)
    {
        try
        {
            var travelTmp = await _unitOfWork.TravelRepository.GetByIdAsync(code);

            if (travelTmp == null)
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Travel());

            var result = await _unitOfWork.TravelRepository.RemoveAsync(travelTmp);

            if (result == false) return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, new Travel());

            return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, travelTmp);
        }
        catch (Exception ex)
        {
            return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
        }

        throw new NotImplementedException();
    }

    public async Task<IBusinessResult> GetAll()
    {
        #region Business role

        #endregion

        var travels = await _unitOfWork.TravelRepository.GetAllAsync();

        if (travels == null) return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);

        return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, travels);
    }

    public async Task<IBusinessResult> GetById(Guid code)
    {
        var travelTmp = await _unitOfWork.TravelRepository.GetByIdAsync(code);

        if (travelTmp == null) return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new Travel());

        return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, travelTmp);
    }

    public async Task<IBusinessResult> Save(Travel currency)
    {
        try
        {
            var result = -1;

            var travelTmp = _unitOfWork.TravelRepository.GetById(currency.Id);

            if (travelTmp != null)
            {
                _unitOfWork.TravelRepository.Context().Entry(travelTmp).CurrentValues.SetValues(currency);

                result = await _unitOfWork.TravelRepository.UpdateAsync(travelTmp);

                if (result > 0)
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
            }

            result = await _unitOfWork.TravelRepository.CreateAsync(currency);

            if (result > 0)
                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
            return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
        }
        catch (Exception ex)
        {
            return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
        }

        throw new NotImplementedException();
    }

    public async Task<IBusinessResult> Update(Travel currency)
    {
        throw new NotImplementedException();
    }
}