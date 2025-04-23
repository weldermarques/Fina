using Fina.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Transactions;

public class UpdateTransactionRequest : Request
{
    [Required(ErrorMessage = "Id inválido")]
    public long Id { get; set; }
    
    [Required(ErrorMessage = "Título inválido")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tipo inválido")]
    public EnumTransactionType Type { get; set; } = EnumTransactionType.Withdrawal;
    
    [Required(ErrorMessage = "Valor inválido")]
    public decimal Ammount { get; set; }
    
    [Required(ErrorMessage = "Data inválida")]
    public DateTime? PaidOrReceivedAt { get; set; }
    
    [Required(ErrorMessage = "Categoria inválida")]
    public long CategoryId { get; set; }
}
