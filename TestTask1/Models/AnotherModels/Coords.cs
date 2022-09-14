﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using static TestTask1.Models.JsonModels.GetCoordsJson;

namespace TestTask1.Models.AnotherModels
{
    public class Coords
    {
        public static async Task<List<double>> GetCoords(string city)
        {
            Root answer;
            List<double> coords = new List<double>();
            System.Net.WebRequest request = WebRequest.Create($"https://geocode-maps.yandex.ru/1.x?apikey=d27a9a97-bd89-413e-aac9-3f109aaf1dcd&geocode={city}&format=json");
            WebResponse response = await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    answer = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.JsonModels.GetCoordsJson.Root>(reader.ReadToEnd());

                }
            }
            response.Close();
            if (answer.response.GeoObjectCollection.featureMember.Count == 0)
                return coords;
            else
            {
                foreach (string point in answer.response.GeoObjectCollection.featureMember[0].GeoObject.Point.pos.Split())
                {
                    coords.Add(Convert.ToDouble(point));
                }
            }
            return coords;
        }
    }
}
