namespace InMemoryGatewayDemo.Entities
{
    public class Equipment : Entity
    {
        public virtual Employee Employee { get; set; }
    }
}
