using ArtonitRestApi.Models;
using ArtonitRestApi.Services;

namespace ArtonitRestApi.Repositories
{
    public class CardRepository
    {
        public int Add(CardBase cardBase, int userId)
        {
            var card = new CardAdd
            {
                UserId = userId
            };

            //запрос надо поменять, это пока не работает
            var rdbDatabase = DatabaseService
               .Get<RDBDatabase>("select GEN_ID (GEN_CARD_ID, 1) from RDB$DATABASE");
            //А в какое поле id записывать?

            card.InitializeFromCardBase(cardBase);

            var result = DatabaseService.Create(card);

            return -1;
        }

        public void Update()
        {

        }

        public void Delete()
        {

        }
    }
}
