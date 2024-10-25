﻿using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Data.Models;
using Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Data.Repository;

namespace Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Data;

public class UnitOfWork
{
    private readonly FA24_SE1717_PRN231_G3_KOIORDERINGSYSTEMINJAPANContext context;
    private TravelRepository travelRepository;
    private UserRepository userRepository;

    public UnitOfWork()
    {
        context ??= new FA24_SE1717_PRN231_G3_KOIORDERINGSYSTEMINJAPANContext();
    }

    public TravelRepository TravelRepository
    {
        get { return travelRepository ??= new TravelRepository(context); }
    }

    public UserRepository UserRepository
    {
        get { return userRepository ??= new UserRepository(context); }
    }

    ////TO-DO CODE HERE/////////////////

    #region Set transaction isolation levels

    /*
    Read Uncommitted: The lowest level of isolation, allows transactions to read uncommitted data from other transactions. This can lead to dirty reads and other issues.

    Read Committed: Transactions can only read data that has been committed by other transactions. This level avoids dirty reads but can still experience other isolation problems.

    Repeatable Read: Transactions can only read data that was committed before their execution, and all reads are repeatable. This prevents dirty reads and non-repeatable reads, but may still experience phantom reads.

    Serializable: The highest level of isolation, ensuring that transactions are completely isolated from one another. This can lead to increased lock contention, potentially hurting performance.

    Snapshot: This isolation level uses row versioning to avoid locks, providing consistency without impeding concurrency.
     */

    public int SaveChangesWithTransaction()
    {
        var result = -1;

        //System.Data.IsolationLevel.Snapshot
        using (var dbContextTransaction = context.Database.BeginTransaction())
        {
            try
            {
                result = context.SaveChanges();
                dbContextTransaction.Commit();
            }
            catch (Exception)
            {
                //Log Exception Handling message                      
                result = -1;
                dbContextTransaction.Rollback();
            }
        }

        return result;
    }

    public async Task<int> SaveChangesWithTransactionAsync()
    {
        var result = -1;

        //System.Data.IsolationLevel.Snapshot
        using (var dbContextTransaction = context.Database.BeginTransaction())
        {
            try
            {
                result = await context.SaveChangesAsync();
                dbContextTransaction.Commit();
            }
            catch (Exception)
            {
                //Log Exception Handling message                      
                result = -1;
                dbContextTransaction.Rollback();
            }
        }

        return result;
    }

    #endregion
}