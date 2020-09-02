using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace jmyoung12_pa1
{
    class Program
    {
        static void Main(string[] args)
        {
            int menuDecision = GetMenuDecision();

            if (menuDecision == 1)
            {
                Console.WriteLine("Big Al's Posts:");

                var posts = ReadPosts();

                DisplayPosts(posts);

                Console.ReadLine();
            }

            else if (menuDecision == 2) 
            {
                Console.WriteLine("What would you like the post to say?");
                string data = Console.ReadLine();
                var posts = ReadPosts();

                posts.Sort(delegate (Post x, Post y)
                {
                    return y.ID.CompareTo(x.ID);
                });

                var newID = 1;

                if (posts.Count > 0)
                    newID = posts[0].ID + 1;

                Post newPost = CreatePost(newID, data);

                posts.Add(newPost);

                SavePosts(posts);

                DisplayPosts(posts);

                Console.ReadLine();
            }

            else if (menuDecision == 3) 
            {
                var posts = ReadPosts();

                DisplayPosts(posts);

                Console.WriteLine("Enter the ID of the post you would like to delete:");
                int id = int.Parse(Console.ReadLine());

                posts.RemoveAt(posts.IndexOf(posts.Find(x => x.ID == id)));

                SavePosts(posts);

                DisplayPosts(posts);

                Console.ReadLine();
            }

            else if (menuDecision == 4) 
            {
                Environment.Exit(0);
            }
        }

        static void DisplayPosts(List<Post> posts)
        {
            posts.Sort(delegate (Post x, Post y)
            {
                if (x.Timestamp == null && y.Timestamp == null) return 0;
                else if (y.Timestamp == null) return -1;
                else if (x.Timestamp == null) return 1;
                else return DateTime.Compare(y.Timestamp, x.Timestamp);
            });

            foreach (var post in posts)
            {
                Console.WriteLine($"{post.ID} - {post.Message} - {post.Timestamp.ToShortDateString()}");
            }
        }

        static int GetMenuDecision()
        {
            Console.WriteLine("\nWelcome to Big Al's Social Media Application!\n");
            Console.WriteLine("1) See All Posts");
            Console.WriteLine("2) Add a Post");
            Console.WriteLine("3) Delete a Post");
            Console.WriteLine("4) Exit");
            int dec = int.Parse(Console.ReadLine());
            while(dec <= 0 || dec >= 5)
            {
                Console.WriteLine("Error! Enter a proper response");
                Console.WriteLine("1) See All Posts");
                Console.WriteLine("2) Add a Post");
                Console.WriteLine("3) Delete a Post");
                Console.WriteLine("4) Exit");
                dec = int.Parse(Console.ReadLine());
            }
            return dec;
        }

        static List<Post> ReadPosts()
        {
            var posts = new List<Post>();

            try          
            {
                using (StreamReader sr = new StreamReader("posts.txt"))
                {
                    string line = sr.ReadLine();

                    while (line != null)
                    {
                        string[] data = line.Split('#');

                        var post = new Post();
                        post.ID = int.Parse(data[0]);
                        post.Message = data[1];
                        post.Timestamp = DateTime.Parse(data[2]);

                        posts.Add(post);

                        line = sr.ReadLine();
                    }

                    posts.Sort(delegate (Post x, Post y)
                    {
                        if (x.Timestamp == null && y.Timestamp == null) return 0;
                        else if (y.Timestamp == null) return -1;
                        else if (x.Timestamp == null) return 1;
                        else return DateTime.Compare(y.Timestamp, x.Timestamp);
                    });
                }
            }
            catch (Exception e)
            {

            }
            return posts;
        }

        static Post CreatePost(int id, string data)
        {
            Post newPost = new Post();

            newPost.ID = id;
            newPost.Message = data;
            newPost.Timestamp = DateTime.Today;

            return newPost;
        }

        static void SavePosts(List<Post> posts)
        {
            if (File.Exists("posts.txt"))
                File.Delete("posts.txt");

            using (StreamWriter sw = new StreamWriter("posts.txt"))
            {
                foreach (var post in posts)
                {
                    string temp = post.ToString();
                    sw.WriteLine(temp);
                }
            }
        }
    }
}
