﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Tables
{
    public class Rating
    {
        public int Id { get; set; }
        public int TotalScore { get; set; }
        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }
        public Item Item { get; set; }
        [ForeignKey(nameof(User))]
        public int? UserId { get; set; }
        public User User { get; set; }
        public ICollection<CategoryRating> CategoryRatings { get; set; }
    }
}
