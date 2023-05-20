﻿using Data.Models;

namespace ApplicationService.DTOs
{
	public class UserDTO
	{
		public int ID { get; set; }
		public string Username { get; set; } = null!;
		public string? Email { get; set; }
		public decimal Points { get; set; }
        public List<int> PostLikes { get; set; } = new();
		public List<int> CommentLikes { get; set; } = new();

        public bool Validate()
		{
			return !String.IsNullOrEmpty(Username) && !String.IsNullOrEmpty(Email);
		}

		public static UserDTO FromUser(User user)
		{
            return new UserDTO()
			{
				ID = user.ID,
				Username = user.Username,
				Email = user.Email,
				Points = user.Points,
				CommentLikes = user.CommentLikes.Select(x => x.CommentID).ToList(),
				PostLikes = user.PostLikes.Select(x => x.PostID).ToList(),
			};
		}
	}
}
