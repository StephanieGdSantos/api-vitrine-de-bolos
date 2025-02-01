namespace CardapioBolos.Banco
{
    public class AdministradorDAL
    {
        private readonly CardapioBolosContext context;
        public AdministradorDAL(CardapioBolosContext context)
        {
            this.context = context;
        }
    }
}
