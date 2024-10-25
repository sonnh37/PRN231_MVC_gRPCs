﻿using Microsoft.EntityFrameworkCore;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Data.Base;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Data.Models;

namespace Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Data.Repository;

public class UserRepository : GenericRepository<User>
{
    public UserRepository()
    {
    }

    public UserRepository(FA24_SE1717_PRN231_G3_KOIORDERINGSYSTEMINJAPANContext testFAContext)
    {
        _context = testFAContext;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Set<User>().ToListAsync();
    }

    public async Task<User?> FindByEmailOrUsername(string keyword)
    {
        IQueryable<User> queryable = _context.Set<User>();

        var user = await queryable.Where(e => e.Email!.ToLower().Trim() == keyword.ToLower().Trim()
                                              || e.Username!.ToLower().Trim() == keyword.ToLower().Trim())
            .SingleOrDefaultAsync();

        return user;
    }
}