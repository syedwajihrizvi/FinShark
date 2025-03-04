using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinShark.DTOs.Stock;
using FinShark.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FinShark.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                CompanyName = stockModel.CompanyName,
                Symbol = stockModel.Symbol,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = [.. stockModel.Comments.Select(c => c.ToCommentDto())]
            };
        }

        public static Stock ToStockFromCreateDTO(this CreateStockRequestDto createStockRequestDto)
        {
            return new Stock
            {
                Symbol = createStockRequestDto.Symbol,
                CompanyName = createStockRequestDto.CompanyName,
                Industry = createStockRequestDto.Industry,
                Purchase = createStockRequestDto.Purchase,
                LastDiv = createStockRequestDto.LastDiv,
                MarketCap = createStockRequestDto.MarketCap
            };
        }
    }
}