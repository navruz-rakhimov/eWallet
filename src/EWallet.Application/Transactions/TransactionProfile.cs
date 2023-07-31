using AutoMapper;
using EWallet.Application.Transactions.Dtos;
using EWallet.Domain.Entities;

namespace EWallet.Application.Transactions;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<TransactionInputDto, Transaction>();
        CreateMap<Transaction, TransactionDto>();
    }
}