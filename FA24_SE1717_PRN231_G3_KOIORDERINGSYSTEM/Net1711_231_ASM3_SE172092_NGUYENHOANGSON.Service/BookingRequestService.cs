using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Common;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Data;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Data.Models;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Service.Base;

namespace Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Service;

public interface IBookingRequestService
{
    Task<IBusinessResult> GetAll();
    Task<IBusinessResult> GetById(Guid id);
    Task<IBusinessResult> Save(BookingRequest bookingRequest);
    Task<IBusinessResult> DeleteById(Guid id);

}
public class BookingRequestService : IBookingRequestService
{
    private readonly UnitOfWork _unitOfWork;
    public BookingRequestService()
    {
        _unitOfWork ??= new UnitOfWork();
    }
    public async Task<IBusinessResult> DeleteById(Guid code)
    {
        try
        {
            var bookingRequest = await _unitOfWork.BookingRequest.GetByIdAsync(code);
            if (bookingRequest == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new BookingRequest());
            }
            else
            {
                var result = await _unitOfWork.BookingRequest.RemoveAsync(bookingRequest);
                if (result)
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, bookingRequest);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, bookingRequest);
                }
            }
        }
        catch (Exception ex)
        {
            return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
        }
    }





    public async Task<IBusinessResult> GetAll()
    {
        var result = await _unitOfWork.BookingRequest.GetAllAsync();
        if (result == null || !result.Any())
        {
            return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, result);
        }
        else
        {
            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
        }
    }



    public async Task<IBusinessResult> GetById(Guid code)
    {
        var bookingRequest = await _unitOfWork.BookingRequest.GetByIdAsync(code);
        if (bookingRequest == null)
        {
            return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new BookingRequest());
        }
        else
        {
            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, bookingRequest);
        }
    }

    public async Task<IBusinessResult> Save(BookingRequest bookingRequest)
    {
        try
        {
            int result = -1;
            var bookingRequestTmp = _unitOfWork.BookingRequest.GetById(bookingRequest.Id);

            if (bookingRequestTmp == null)
            {
                result = await _unitOfWork.BookingRequest.CreateAsync(bookingRequest);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, result);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, new BookingRequest());
                }
            }
            else
            {
                _unitOfWork.BookingRequest.Context().Entry(bookingRequestTmp).CurrentValues.SetValues(bookingRequest);
                result = await _unitOfWork.BookingRequest.UpdateAsync(bookingRequestTmp);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, result);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, new BookingRequest());
                }
            }
        }
        catch (Exception ex)
        {
            return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
        }
    }

}