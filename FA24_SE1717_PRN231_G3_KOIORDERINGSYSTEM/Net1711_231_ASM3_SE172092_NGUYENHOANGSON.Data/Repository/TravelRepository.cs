using Microsoft.EntityFrameworkCore;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Data.Base;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Data.Models;

namespace Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Data.Repository;

public class TravelRepository : GenericRepository<Travel>
{
    public TravelRepository() { }

    public TravelRepository(FA24_SE1717_PRN231_G3_KOIORDERINGSYSTEMINJAPANContext context) => _context = context;

}

