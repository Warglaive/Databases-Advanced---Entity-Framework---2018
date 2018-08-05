using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Instagraph.Data;
using Instagraph.Models;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            var deserializePictures = JsonConvert.DeserializeObject<Picture[]>(jsonString);
            var pictures = new List<Picture>();
            var sb = new StringBuilder();
            foreach (var picture in deserializePictures)
            {
                if (IsValid(picture, deserializePictures))
                {
                    pictures.Add(picture);
                    sb.AppendLine($"Successfully imported Picture {picture.Path}.");
                }
                else
                {
                    sb.AppendLine("Error: Invalid data.");
                }
            }

            Console.WriteLine(sb.ToString().Trim());
            context.Pictures.AddRange(pictures);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            throw new NotImplementedException();
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            throw new NotImplementedException();
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            throw new NotImplementedException();
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            throw new NotImplementedException();
        }
        //if not working in judge go for IsValid with validator
        public static bool IsValid(Picture currentPicture, Picture[] pictures)
        {
            var counter = 0;
            if (!string.IsNullOrEmpty(currentPicture.Path)
                && currentPicture.Size > 0)
            {
                foreach (var picture in pictures)
                {
                    if (picture.Path == currentPicture.Path)
                    {
                        counter++;
                    }
                }

                if (counter > 1)
                {
                    return false;
                }

                return true;
            }

            return false;
        }
    }
}