using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.RawModels;
using Newtonsoft.Json.Linq;

namespace Logic
{
    public class Mapper
    {
        private readonly string _mapboxurl = "https://api.mapbox.com";
        private readonly string _mapboxtoken = "access_token=sk.eyJ1Ijoibm90YXNhbGFkIiwiYSI6ImNrbmFucHY5MDBmNWgydmxsZTBsdmZrY3oifQ._ZzlvInwLBJvA6gGIqdLPA";
        public Mapper()
        {

        }

        internal User RawToUser(RawUser rUser)
        {
            User newUser = new User();
            using (var hmac = new HMACSHA512())
            {
                newUser.FName = rUser.firstName;
                newUser.LName = rUser.lastName;
                newUser.IsEventManager = false;
                newUser.Email = rUser.email.ToLower();
                newUser.PasswordSalt = hmac.Key;
                newUser.Password = PasswordHash(rUser.password, newUser.PasswordSalt);
            }
            return newUser;
        }

        internal async Task<Location> AddressToLocation(RawEvent userEvent)
        {
            HttpClient client = new HttpClient();
            string path = _mapboxurl + "/geocoding/v5/mapbox.places/"+userEvent.Street+"%20"+userEvent.ZipCode+"%20"+userEvent.City+"%20"+userEvent+userEvent.State+".json?country=US&"+_mapboxtoken;
            HttpResponseMessage response = await client.GetAsync(path);
            if(response.IsSuccessStatusCode)
            {
                Location newLoc = new Location();
                string jsonContent = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(jsonContent);
                //Console.WriteLine(json["features"][0]["center"][0] + " - " + json["features"][0]["center"][1]);
                newLoc.Name = userEvent.Street;
                newLoc.Address = userEvent.City + " " + userEvent.State + " " + userEvent.ZipCode;
                newLoc.Longtitude = (double)json["features"][0]["center"][0];
                newLoc.Latitude = (double)json["features"][0]["center"][1];
                return newLoc;
            }
            else
            {
                return null;
            }
        }

        internal async Task<Event> RawToEvent(RawEvent userEvent, EventType eventType, Location loc, User manager)
        {
            Event newEvent = new Event();
            newEvent.Name = userEvent.Name;
            newEvent.Date = userEvent.Date;
            newEvent.Description = userEvent.Description;
            newEvent.Capacity = userEvent.Capacity;
            newEvent.EventType = eventType;
            newEvent.Location = loc;
            newEvent.Manager = manager;
            return newEvent;
        }

        public byte[] PasswordHash(string password, byte[] salt)
        {
            using HMACSHA512 hmac = new HMACSHA512(key: salt);

            var hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return hashedPassword;
        }

        internal EventType StringToEventType(string t)
        {
            EventType newType = new EventType();
            newType.Name = t;
            return newType;
        }
    }
}