using System;
using System.Collections.Generic;
using Domain.Models;
using Domain.RawModels;
using Repository;

namespace Logic
{
    public class EventLogic
    {
        private readonly TestRepository testRepo;
        private readonly Mapper mapper = new Mapper();
        public EventLogic(TestRepository r)
        {
            testRepo = r;
        }

        /// <summary>
        /// Gets all events that are coming up
        /// </summary>
        /// <returns></returns>
        public List<Event> GetUpcomingEvents()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all reviews of an event based off eventID
        /// </summary>
        /// <param name="id">Event ID</param>
        /// <returns></returns>
        public List<Review> GetAllReviews(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all events a user has been to from database based off userID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns></returns>
        public List<Event> GetAllPreviousEvents(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all events a user signed up for from the database based off userID
        /// </summary>
        /// /// <param name="id">User ID</param>
        /// <returns></returns>
        public List<Event> GetAllSignedUpEvents(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}