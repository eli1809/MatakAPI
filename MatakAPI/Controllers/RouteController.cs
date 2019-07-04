﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MatakDBConnector;
using MatakAPI.Models;
//using BAMCIS.GeoJSON;
using Newtonsoft.Json;
using GeoJSON.Net.Feature;

namespace MatakAPI.Controllers
{
    [Route("api/Route")]
    [ApiController]
    public class RouteController : Controller
    {
        /*
        // POST: /api/Route/setGeoJSON
        [HttpGet("setGeoJSON")]
        [HttpPost]
        public IActionResult setGeoJSON([FromBody]string geojson)
        {
            return Ok(geojson);
        }

        */
        // POST: /api/Route/setRoute
        [HttpPost("SetRoute")]
        public IActionResult setRoute([FromBody] Route newRoute)
        {

            string errorString = null;
            int count = 0;
            RouteModel RouteModel = new RouteModel();
            OrganizationModel orgModel = new OrganizationModel();
            count = RouteModel.GetRoutesCountByOrgId(newRoute.OrgId,out errorString);
            newRoute.Name = orgModel.getOrganizationById(newRoute.OrgId, out errorString).Name;
            newRoute.Name += " "+count+1;
            newRoute.RouteId = RouteModel.AddNewRoute(newRoute, out errorString);
            RouteObj obj = new RouteObj(newRoute);
            return new JsonResult(obj);


        }

        [HttpPost("UpdateRoute")]
        public IActionResult UpdateRoute([FromBody] Route newRoute)
        {

            string errorString = null;
            RouteModel routeModel = new RouteModel();
            if (newRoute.RouteId == 0)
                return BadRequest("please enter ID");
            newRoute.RouteId = routeModel.UpdateRouteId(newRoute, out errorString);
            RouteObj obj = new RouteObj(newRoute);
            return new JsonResult(obj);


        }


        // POST: /api/Route/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAllRoutes()
        {
           
            string errorString = null;
            RouteModel RouteCont = new RouteModel();
            List<Route> obj = RouteCont.GetAllRoutes(out errorString);
            return new JsonResult(obj);



        }

        // Get: /api/Route/5
        [HttpGet("{id}")]    
        public IActionResult GetRoute(int id)
        {
           
            string errorString = null;
            RouteModel RouteModel = new RouteModel();
            
            Route obj = RouteModel.GetRouteById(id, out errorString);
            if (obj != null && errorString == null)
            {
                return new JsonResult(obj);
            }
            else
            {
                return BadRequest("cant find");
            }

        }

        [HttpPost("GetReasons")]
        public IActionResult GetReasons()
        {
            string errorString = null;
            ReasonModel reasonModel = new ReasonModel();
            List<Reason> obj = reasonModel.GetAllReasons(out errorString);
            return new JsonResult(obj);
        }

    }
}