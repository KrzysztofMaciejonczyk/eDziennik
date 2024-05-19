using System;
using System.Net;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;



namespace EDziennik.Controllers.Baza
{
  using Models;
  using Data;
  using Models.Baza;

  [Route("odata/Baza/Przedmiots")]
  public partial class PrzedmiotsController : ODataController
  {
    private Data.BazaContext context;

    public PrzedmiotsController(Data.BazaContext context)
    {
      this.context = context;
    }
    // GET /odata/Baza/Przedmiots
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Baza.Przedmiot> GetPrzedmiots()
    {
      var items = this.context.Przedmiots.AsQueryable<Models.Baza.Przedmiot>();
      this.OnPrzedmiotsRead(ref items);

      return items;
    }

    partial void OnPrzedmiotsRead(ref IQueryable<Models.Baza.Przedmiot> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/Baza/Przedmiots(przedmiot_id={przedmiot_id})")]
    public SingleResult<Przedmiot> GetPrzedmiot(int key)
    {
        var items = this.context.Przedmiots.Where(i=>i.przedmiot_id == key);
        this.OnPrzedmiotsGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnPrzedmiotsGet(ref IQueryable<Models.Baza.Przedmiot> items);

    partial void OnPrzedmiotDeleted(Models.Baza.Przedmiot item);

    [HttpDelete("/odata/Baza/Przedmiots(przedmiot_id={przedmiot_id})")]
    public IActionResult DeletePrzedmiot(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var itemToDelete = this.context.Przedmiots
                .Where(i => i.przedmiot_id == key)
                .FirstOrDefault();

            if (itemToDelete == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            this.OnPrzedmiotDeleted(itemToDelete);
            this.context.Przedmiots.Remove(itemToDelete);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnPrzedmiotUpdated(Models.Baza.Przedmiot item);

    [HttpPut("/odata/Baza/Przedmiots(przedmiot_id={przedmiot_id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutPrzedmiot(int key, [FromBody]Models.Baza.Przedmiot newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.przedmiot_id != key))
            {
                return BadRequest();
            }

            this.OnPrzedmiotUpdated(newItem);
            this.context.Przedmiots.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Przedmiots.Where(i => i.przedmiot_id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Nauczyciel,Klasa");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/Baza/Przedmiots(przedmiot_id={przedmiot_id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchPrzedmiot(int key, [FromBody]Delta<Models.Baza.Przedmiot> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemToUpdate = this.context.Przedmiots.Where(i => i.przedmiot_id == key).FirstOrDefault();

            if (itemToUpdate == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            patch.Patch(itemToUpdate);

            this.OnPrzedmiotUpdated(itemToUpdate);
            this.context.Przedmiots.Update(itemToUpdate);
            this.context.SaveChanges();

            var itemToReturn = this.context.Przedmiots.Where(i => i.przedmiot_id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Nauczyciel,Klasa");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnPrzedmiotCreated(Models.Baza.Przedmiot item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Baza.Przedmiot item)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (item == null)
            {
                return BadRequest();
            }

            this.OnPrzedmiotCreated(item);
            this.context.Przedmiots.Add(item);
            this.context.SaveChanges();

            var key = item.przedmiot_id;

            var itemToReturn = this.context.Przedmiots.Where(i => i.przedmiot_id == key);

            Request.QueryString = Request.QueryString.Add("$expand", "Nauczyciel,Klasa");

            return new ObjectResult(SingleResult.Create(itemToReturn))
            {
                StatusCode = 201
            };
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
