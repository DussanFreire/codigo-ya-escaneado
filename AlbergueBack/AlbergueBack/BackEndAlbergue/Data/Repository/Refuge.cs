using BackEndAlbergue.Data.Entities;
using BackEndAlbergue.Models.Auths;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAlbergue.Data.Repository
{
    public class Refuge : IRefuge
    {
        static string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=albergues;";
        static string query = "SELECT * FROM pets";
        private PetEntity _firstPet = null;
        private PetEntity _lastPet = null;

        public Refuge()
        {
            checkFirstPet();
            checkLastPet();
        }

        private void checkLastPet()
        {
            _lastPet = new PetEntity();
            MySqlConnection databaseConnection2 = new MySqlConnection(connectionString);
            string localQuery = $"SELECT * from pets order by id desc limit 1;";
            MySqlCommand commandDatabase2 = new MySqlCommand(localQuery, databaseConnection2);
            commandDatabase2.CommandTimeout = 60;
            databaseConnection2.Open();
            MySqlDataReader _reader2 = commandDatabase2.ExecuteReader();
            if (_reader2.HasRows)
            {
                while (_reader2.Read())
                {
                    _lastPet.Id = _reader2.GetInt32(0);
                    _lastPet.Name = _reader2.GetString(1);
                    _lastPet.Sex = _reader2.GetChar(2);
                    _lastPet.Description = _reader2.GetString(3);
                    _lastPet.Photo = _reader2.GetString(4);
                    _lastPet.Vaccines = _reader2.GetString(5);
                    _lastPet.Sterilization = _reader2.GetBoolean(6);
                    _lastPet.Size = _reader2.GetString(7);
                    _lastPet.age = _reader2.GetDateTime(8);
                    _lastPet.IsAdopted = _reader2.GetBoolean(9);
                    _lastPet.next = IsThereANextDog(_reader2.IsDBNull(10), _reader2.GetInt32(10));
                    _lastPet.previous = IsThereAPrevDog(_reader2.IsDBNull(11), _reader2.GetInt32(11));
                }
                databaseConnection2.Close();
            }
            if (_lastPet.Name == null)
            {
                _lastPet = null;
            }
        }

        private void checkFirstPet()
        {
            _firstPet = new PetEntity();
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            string localQuery = $"SELECT * from pets order by id asc limit 1;";
            MySqlCommand commandDatabase = new MySqlCommand(localQuery, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            databaseConnection.Open();
            MySqlDataReader _reader = commandDatabase.ExecuteReader();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    _firstPet.Id = _reader.GetInt32(0);
                    _firstPet.Name = _reader.GetString(1);
                    _firstPet.Sex = _reader.GetChar(2);
                    _firstPet.Description = _reader.GetString(3);
                    _firstPet.Photo = _reader.GetString(4);
                    _firstPet.Vaccines = _reader.GetString(5);
                    _firstPet.Sterilization = _reader.GetBoolean(6);
                    _firstPet.Size = _reader.GetString(7);
                    _firstPet.age = _reader.GetDateTime(8);
                    _firstPet.IsAdopted = _reader.GetBoolean(9);
                    _firstPet.next = IsThereANextDog(_reader.IsDBNull(10), _reader.GetInt32(11));
                    _firstPet.previous = IsThereAPrevDog(_reader.IsDBNull(10), _reader.GetInt32(11));

                }
                databaseConnection.Close();
            }
            if (_firstPet.Name == null)
            {
                _firstPet = null;
            }
        }

        private int? IsThereAPrevDog(bool next, int val)
        {
            if (next)
            {
                return null;
            }
            return val;
        }

        private int? IsThereANextDog(bool prev, int val)
        {
            if (prev)
            {
                return null;
            }
            return val;
        }

        public IEnumerable<PetEntity> GetPets()
        {
            List<PetEntity> pets = new List<PetEntity>();
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            databaseConnection.Open();
            MySqlDataReader _reader = commandDatabase.ExecuteReader();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    PetEntity pet = new PetEntity();
                    pet.Id = _reader.GetInt32(0);
                    pet.Name = _reader.GetString(1);
                    pet.Sex = _reader.GetChar(2);
                    pet.Description = _reader.GetString(3);
                    pet.Photo = _reader.GetString(4);
                    pet.Vaccines = _reader.GetString(5);
                    pet.Sterilization = _reader.GetBoolean(6);
                    pet.Size = _reader.GetString(7);
                    pet.age = _reader.GetDateTime(8);
                    pet.IsAdopted = _reader.GetBoolean(9);
                    pets.Add(pet);
                }
                databaseConnection.Close();
            }
            return pets;
        }

        public PetEntity GetPet(int petId)
        {
            PetEntity pet = new PetEntity();
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            string localQuery = $"SELECT * FROM pets where Id = {petId}";
            MySqlCommand commandDatabase = new MySqlCommand(localQuery, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            databaseConnection.Open();
            MySqlDataReader _reader = commandDatabase.ExecuteReader();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    
                    pet.Id = _reader.GetInt32(0);
                    pet.Name = _reader.GetString(1);
                    pet.Sex = _reader.GetChar(2);
                    pet.Description = _reader.GetString(3);
                    pet.Photo = _reader.GetString(4);
                    pet.Vaccines = _reader.GetString(5);
                    pet.Sterilization = _reader.GetBoolean(6);
                    pet.Size = _reader.GetString(7);
                    pet.age = _reader.GetDateTime(8);
                    pet.IsAdopted = _reader.GetBoolean(9);
                    if(_reader.IsDBNull(10))
                    {
                        pet.next = null;
                    }
                    else
                    {
                        pet.next=_reader.GetInt32(10);
                    }
                    if (_reader.IsDBNull(11))
                    {
                        pet.previous = null;
                    }
                    else
                    {
                        pet.previous = _reader.GetInt32(11);
                    }
                }
                databaseConnection.Close();
            }
            return pet;
        }
        public PetEntity GetPetNullable(int? petId)
        {
            PetEntity pet = new PetEntity();
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            string localQuery = $"SELECT * FROM pets where Id = {petId}";
            MySqlCommand commandDatabase = new MySqlCommand(localQuery, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            databaseConnection.Open();
            MySqlDataReader _reader = commandDatabase.ExecuteReader();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    pet.Id = _reader.GetInt32(0);
                    pet.Name = _reader.GetString(1);
                    pet.Sex = _reader.GetChar(2);
                    pet.Description = _reader.GetString(3);
                    pet.Photo = _reader.GetString(4);
                    pet.Vaccines = _reader.GetString(5);
                    pet.Sterilization = _reader.GetBoolean(6);
                    pet.Size = _reader.GetString(7);
                    pet.age = _reader.GetDateTime(8);
                    pet.IsAdopted = _reader.GetBoolean(9);
                    if (_reader.IsDBNull(10))
                    {
                        pet.next = null;
                    }
                    else
                    {
                        pet.next = _reader.GetInt32(10);
                    }
                    if (_reader.IsDBNull(11))
                    {
                        pet.previous = null;
                    }
                    else
                    {
                        pet.previous = _reader.GetInt32(11);
                   
                    }
                }
                databaseConnection.Close();
            }
            return pet;
        }
        public PetEntity CreatePet(PetEntity petEntity)
        {
            int sterilization = (bool)petEntity.Sterilization ? 1 : 0;
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            databaseConnection.Open();
            MySqlCommand commandDatabase = new MySqlCommand($"Insert into  pets(Name, Sex, Description,Size,Vaccines,Sterilization,age,Photo,IsAdopted) values( '{petEntity.Name}','{petEntity.Sex}', '{petEntity.Description}', '{petEntity.Size}', '{petEntity.Vaccines}','{sterilization}','{petEntity.age.Value.ToString("yyyy-MM-dd HH:mm:ss")}','{petEntity.Photo}','{petEntity.IsAdopted}' )", databaseConnection);
            commandDatabase.ExecuteNonQuery();
            databaseConnection.Close();


            MySqlConnection databaseConnectionGetId = new MySqlConnection(connectionString);
            string localQuery = $"SELECT * FROM pets where Name = '{petEntity.Name}' and sex= '{petEntity.Sex}' and Description = '{petEntity.Description}' ";
            MySqlCommand commandDatabaseGetId = new MySqlCommand(localQuery, databaseConnectionGetId);
            commandDatabaseGetId.CommandTimeout = 60;
            databaseConnectionGetId.Open();
            MySqlDataReader _reader = commandDatabaseGetId.ExecuteReader();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    petEntity.Id = _reader.GetInt32(0);
                }
                databaseConnectionGetId.Close();
            }

            if(_firstPet==null && _lastPet==null)
            {
                _firstPet = petEntity;
                _lastPet = petEntity;
            }
            else
            {
                _lastPet.next = petEntity.Id;
                MySqlConnection databaseConnectionUpdate = new MySqlConnection(connectionString);
                databaseConnectionUpdate.Open();
                MySqlCommand comando = new MySqlCommand($"Update pets set next = '{petEntity.Id}'  where Id = { _lastPet.Id }", databaseConnectionUpdate);
                comando.ExecuteNonQuery();
                databaseConnectionUpdate.Close();

                petEntity.previous = _lastPet.Id;
                MySqlConnection databaseConnectionUpdate2 = new MySqlConnection(connectionString);
                databaseConnectionUpdate2.Open();
                MySqlCommand comando2 = new MySqlCommand($"Update pets set previous = '{_lastPet.Id}'  where Id = { petEntity.Id }", databaseConnectionUpdate2);
                comando2.ExecuteNonQuery();
                databaseConnectionUpdate2.Close();
                _lastPet = petEntity;
            }

            return petEntity;
        }

        public bool DeletePet(int petId)
        {
            bool res = false;
            PetEntity thisPet = GetPet(petId);


            int retorno;
            if (thisPet.Id == _lastPet.Id && thisPet.Id == _firstPet.Id)
            {
                _firstPet = null;
                _lastPet = null;
            }
            else
            {
                if (thisPet.Id == _lastPet.Id)
                {
                    _lastPet = GetPetNullable(thisPet.previous);
                    _lastPet.next = null;

                    MySqlConnection databaseConnectionUpdate3 = new MySqlConnection(connectionString);
                    databaseConnectionUpdate3.Open();
                    databaseConnectionUpdate3.Close();
                }
                else
                {
                    if (thisPet.Id == _firstPet.Id)
                    {
                        _firstPet = GetPetNullable(thisPet.next);
                        _firstPet.previous = null;
                        MySqlConnection databaseConnectionUpdate4 = new MySqlConnection(connectionString);
                        databaseConnectionUpdate4.Open();
                        MySqlCommand comando4 = new MySqlCommand($"Update pets set previous = NULL  where Id = { _firstPet.Id }", databaseConnectionUpdate4);
                        retorno = comando4.ExecuteNonQuery();
                        databaseConnectionUpdate4.Close();
                    }
                    else
                    {
                        PetEntity thisPetNext = GetPetNullable(thisPet.next);
                        PetEntity thisPetPrevious = GetPetNullable(thisPet.previous);
                        thisPetNext.previous = thisPet.previous;
                        MySqlConnection databaseConnectionUpdate2 = new MySqlConnection(connectionString);
                        databaseConnectionUpdate2.Open();
                        MySqlCommand comando2 = new MySqlCommand($"Update pets set previous = '{thisPetPrevious.Id}'  where Id = { thisPetNext.Id }", databaseConnectionUpdate2);
                        retorno = comando2.ExecuteNonQuery();
                        databaseConnectionUpdate2.Close();


                        thisPetPrevious.next = thisPet.next;
                        MySqlConnection databaseConnectionUpdate = new MySqlConnection(connectionString);
                        databaseConnectionUpdate.Open();
                        MySqlCommand comando = new MySqlCommand($"Update pets set next = '{thisPetNext.Id}'  where Id = { thisPetPrevious.Id }", databaseConnectionUpdate);
                        retorno = comando.ExecuteNonQuery();
                        databaseConnectionUpdate.Close();
                    }
                }
            }



            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            databaseConnection.Open();
            string localQuery = $"Delete From pets where Id ={ petId }";
            MySqlCommand commandDatabase = new MySqlCommand(localQuery, databaseConnection);
            retorno = commandDatabase.ExecuteNonQuery();
            databaseConnection.Close();
            if (retorno == 1)
                res = true;
            return res;
        }

        public PetEntity UpdatePet(PetEntity petEntity)
        {
            var petToUpdate = GetPet(petEntity.Id);
            petToUpdate.Name = petEntity.Name ?? petToUpdate.Name;
            petToUpdate.Sex = petEntity.Sex ?? petToUpdate.Sex;
            petToUpdate.Description = petEntity.Description ?? petToUpdate.Description;
            petToUpdate.Size = petEntity.Size ?? petToUpdate.Size;
            petToUpdate.Photo = petEntity.Photo ?? petToUpdate.Photo;
            petToUpdate.Vaccines = petEntity.Vaccines ?? petToUpdate.Vaccines;
            petToUpdate.Sterilization = petEntity.Sterilization ?? petToUpdate.Sterilization;
            petToUpdate.age = petEntity.age ?? petToUpdate.age;
            petToUpdate.IsAdopted = petEntity.IsAdopted ?? petToUpdate.IsAdopted;

            int sterilization = (bool)petToUpdate.Sterilization ? 1 : 0;
            int isAdopted = (bool)petToUpdate.IsAdopted ? 1 : 0;

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            databaseConnection.Open();
            MySqlCommand comando = new MySqlCommand($"Update pets set Name = '{petToUpdate.Name}', Sex='{ petToUpdate.Sex }', Description = '{petToUpdate.Description}', Size = '{petToUpdate.Size}', Vaccines='{petToUpdate.Vaccines}',Sterilization={sterilization}, age = '{petToUpdate.age.Value.ToString("yyyy-MM-dd HH:mm:ss")}', Photo='{petToUpdate.Photo}', IsAdopted={isAdopted}  where Id = { petToUpdate.Id }", databaseConnection);
            comando.ExecuteNonQuery();
            databaseConnection.Close();
            return petEntity;
        }
        public IEnumerable<NoticeEntity> GetNotices()
        {
            List<NoticeEntity> notices = new List<NoticeEntity>();
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            string localQuery = $"SELECT * FROM notice";
            MySqlCommand commandDatabase = new MySqlCommand(localQuery, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            databaseConnection.Open();
            MySqlDataReader _reader = commandDatabase.ExecuteReader();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    NoticeEntity notice = new NoticeEntity();
                    notice.id = _reader.GetInt32(0);
                    notice.title = _reader.GetString(1);
                    notice.image = _reader.GetString(2);
                    notice.date = _reader.GetDateTime(3);
                    notices.Add(notice);
                }
                databaseConnection.Close();
            }
            return notices;
        }
        public NoticeEntity GetNotice(int NoticeId)
        {
            NoticeEntity notice = new NoticeEntity();
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            string localQuery = $"SELECT * FROM notice where id = {NoticeId}";
            MySqlCommand commandDatabase = new MySqlCommand(localQuery, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            databaseConnection.Open();
            MySqlDataReader _reader = commandDatabase.ExecuteReader();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    notice.id = _reader.GetInt32(0);
                    notice.title = _reader.GetString(1);
                    notice.image = _reader.GetString(2);
                    notice.date = _reader.GetDateTime(3);
                }
                databaseConnection.Close();
            }
            return notice;
        }
        public NoticeEntity CreateNotice(NoticeEntity noticeEntity)
        {
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            databaseConnection.Open();
            MySqlCommand commandDatabase = new MySqlCommand($"Insert into  notice(title, image, date) values( '{noticeEntity.title}','{noticeEntity.image}','{noticeEntity.date.Value.ToString("yyyy-MM-dd HH:mm:ss")}' )", databaseConnection);
            commandDatabase.ExecuteNonQuery();
            databaseConnection.Close();
            return noticeEntity;
        }

        public bool DeleteNotice(int NoticeId)
        {
            int retorno;
            bool res = false;
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            databaseConnection.Open();
            string localQuery = $"Delete From notice where id ={ NoticeId }";
            MySqlCommand commandDatabase = new MySqlCommand(localQuery, databaseConnection);
            retorno = commandDatabase.ExecuteNonQuery();
            databaseConnection.Close();
            if (retorno == 1)
                res = true;
            return res;
        }

        public NoticeEntity UpdateNotice(NoticeEntity noticeEntity)
        {
            var noticeToUpdate = GetNotice(noticeEntity.id);
            noticeToUpdate.title = noticeEntity.title ?? noticeToUpdate.title;
            noticeToUpdate.image = noticeEntity.image ?? noticeToUpdate.image;
            noticeToUpdate.date = noticeEntity.date ?? noticeToUpdate.date;

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            databaseConnection.Open();

            MySqlCommand comando = new MySqlCommand($"Update notice set title = '{noticeToUpdate.title}', image='{noticeToUpdate.image}', date = '{noticeToUpdate.date.Value.ToString("yyyy-MM-dd HH:mm:ss")}' where id = {noticeToUpdate.id}", databaseConnection);
            comando.ExecuteNonQuery();
            databaseConnection.Close();
            return noticeEntity;
        }

        public IEnumerable<ProductEntity> GetProducts()
        {
            List<ProductEntity> petShopItems = new List<ProductEntity>();
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            string localQuery = $"SELECT * FROM petShop";
            MySqlCommand commandDatabase = new MySqlCommand(localQuery, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            databaseConnection.Open();
            MySqlDataReader _reader = commandDatabase.ExecuteReader();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    ProductEntity petShopItem = new ProductEntity();
                    petShopItem.Id = _reader.GetInt32(0);
                    petShopItem.ProductName = _reader.GetString(1);
                    petShopItem.Sex = _reader.GetChar(2);
                    petShopItem.Photo = _reader.GetString(3);
                    petShopItem.Category = _reader.GetString(4);
                    petShopItem.SizeOfPet = _reader.GetString(5);
                    petShopItem.Price = _reader.GetString(6);
                    petShopItem.Stock = _reader.GetInt32(7);
                    petShopItems.Add(petShopItem);
                }
                databaseConnection.Close();
            }
            return petShopItems;
        }
        public ProductEntity GetProduct(int ItemId)
        {
            ProductEntity petShopItem = new ProductEntity();
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            string localQuery = $"SELECT * FROM petShop where id = {ItemId}";
            MySqlCommand commandDatabase = new MySqlCommand(localQuery, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            databaseConnection.Open();
            MySqlDataReader _reader = commandDatabase.ExecuteReader();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    petShopItem.Id = _reader.GetInt32(0);
                    petShopItem.ProductName = _reader.GetString(1);
                    petShopItem.Sex = _reader.GetChar(2);
                    petShopItem.Photo = _reader.GetString(3);
                    petShopItem.Category = _reader.GetString(4);
                    petShopItem.SizeOfPet = _reader.GetString(5);
                    petShopItem.Price = _reader.GetString(6);
                    petShopItem.Stock = _reader.GetInt32(7);
                }
                databaseConnection.Close();
            }
            return petShopItem;
        }
        public ProductEntity CreateProduct(ProductEntity productEntity)
        {
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            databaseConnection.Open();
            MySqlCommand commandDatabase = new MySqlCommand($"Insert into  petShop(ProductName, Sex, Photo, Category, SizeOfPet, Price, Stock) values( '{productEntity.ProductName}','{productEntity.Sex}','{productEntity.Photo}','{productEntity.Category}','{productEntity.SizeOfPet}','{productEntity.Price}',{productEntity.Stock})", databaseConnection);
            commandDatabase.ExecuteNonQuery();
            databaseConnection.Close();
            return productEntity;
        }

        public bool DeleteProduct(int ItemId)
        {
            int retorno;
            bool res = false;
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            databaseConnection.Open();
            string localQuery = $"Delete From petShop where id ={ ItemId }";
            MySqlCommand commandDatabase = new MySqlCommand(localQuery, databaseConnection);
            retorno = commandDatabase.ExecuteNonQuery();
            databaseConnection.Close();
            if (retorno == 1)
                res = true;
            return res;
        }

        public ProductEntity UpdateProduct(ProductEntity productEntity)
        {
            var petShopItemToUpdate = GetProduct(productEntity.Id);
            petShopItemToUpdate.ProductName = productEntity.ProductName ?? petShopItemToUpdate.ProductName;
            petShopItemToUpdate.Sex = productEntity.Sex ?? petShopItemToUpdate.Sex;
            petShopItemToUpdate.Photo = productEntity.Photo ?? petShopItemToUpdate.Photo;
            petShopItemToUpdate.Category = productEntity.Category ?? petShopItemToUpdate.Category;
            petShopItemToUpdate.SizeOfPet = productEntity.SizeOfPet ?? petShopItemToUpdate.SizeOfPet;
            petShopItemToUpdate.Price = productEntity.Price ?? petShopItemToUpdate.Price;
            petShopItemToUpdate.Stock = productEntity.Stock ?? petShopItemToUpdate.Stock;

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            databaseConnection.Open();
            MySqlCommand comando = new MySqlCommand($"Update petShop set ProductName = '{petShopItemToUpdate.ProductName}', Sex='{petShopItemToUpdate.Sex}', Photo = '{petShopItemToUpdate.Photo}', Category = '{petShopItemToUpdate.Category}', SizeOfPet = '{petShopItemToUpdate.SizeOfPet}', Price = '{petShopItemToUpdate.Price}', Stock = '{petShopItemToUpdate.Stock}' where id = {petShopItemToUpdate.Id}", databaseConnection);
            comando.ExecuteNonQuery();
            databaseConnection.Close();
            return productEntity;
        }
        public IEnumerable<UserModel> GetUsers()
        {
            List<UserModel> users = new List<UserModel>();
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            string localQuery = $"SELECT * FROM aspnetusers";
            MySqlCommand commandDatabase = new MySqlCommand(localQuery, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            databaseConnection.Open();
            MySqlDataReader _reader = commandDatabase.ExecuteReader();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    UserModel user = new UserModel();
                    user.Id = _reader.GetString(0);
                    user.UserName = _reader.GetString(1);
                    user.Email = _reader.GetString(3);
                    user.PasswordHash = _reader.GetString(6);
                    users.Add(user);
                }
                databaseConnection.Close();
            }
            return users;
        }

    }
}
