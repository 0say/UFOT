namespace UFOT.Models
{
    public class Movimiento
    {
            public int IDTransaccion { get; set; }
            public decimal Monto { get; set; }
            public DateTime FechaTransaccion { get; set; }
            public string Descripcion { get; set; }
        
    }
}
