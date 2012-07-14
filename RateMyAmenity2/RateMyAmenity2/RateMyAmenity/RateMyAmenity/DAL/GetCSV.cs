﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RateMyAmenity.Models;
using RateMyAmenity.DataImport;
using System.IO;
using System.Net;

namespace RateMyAmenity.DAL
{
    public class GetCSV : DropCreateDatabaseIfModelChanges<RateMyAmenityContext>
    {
        protected override void Seed(RateMyAmenityContext context)
        {

            // Add uri and type for each amenity to a multidimensional array.
            string[,] amenities = new string[,] { 
            
                {"http://data.fingal.ie/datasets/csv/Arts_Centres.csv", "Art Centres" },  
                {"http://data.fingal.ie/datasets/csv/Cinemas.csv", "Cinemas" }, 
                {"http://data.fingal.ie/datasets/csv/Heritage_Venues.csv", "Heritage Venues"},
                {"http://data.fingal.ie/datasets/csv/Parks.csv", "Parks" },
                { "http://data.fingal.ie/datasets/csv/Play_Areas.csv", "Play Areas" }
            };

            //  loop through each amenity, parse using the csvparser & add to the DB.
            for (int i = 0; i < amenities.Length / 2; i++)
            {
                IDataParser ourcsv = new CSVParser();

                //  code to fetch the csv from a URI instead of locally.  uses system.net.
                HttpWebRequest reqFP = (HttpWebRequest)HttpWebRequest.Create(amenities[i, 0]);
                HttpWebResponse rspFP = (HttpWebResponse)reqFP.GetResponse();
                // Stream st = rspFP.GetResponseStream();


                StreamReader tmp = new StreamReader(rspFP.GetResponseStream());
                ourcsv.setStreamSource(tmp);

                // pass the amenity type to parseAmenity
                var Amenities = ourcsv.parseAmenity(amenities[i, 1]);

                //  Add and save data to the DB.
                Amenities.ForEach(s => context.Amenities.Add(s));
                context.SaveChanges();

            }

            
        }
    }
}