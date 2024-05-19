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

  [Route("odata/Baza/Rodzics")]
  public partial class RodzicsController : ODataController
  {
    private Data.BazaContext context;

    public RodzicsController(Data.BazaContext context)
    {
      this.context = context;
    }
    // GET /odata/Baza/Rodzics
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Baza.Rodzic> GetRodzics()
    {
      var items = this.context.Rodzics.AsQueryable<Models.Baza.Rodzic>();
      this.OnRodzicsRead(ref items);

      return items;
    }

    partial void OnRodzicsRead(ref IQueryable<Models.Baza.Rodzic> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/Baza/Rodzics(rodzic_id={rodzic_id})")]
    public SingleResult<Rodzic> GetRodzic(int key)
    {
        var items = this.context.Rodzics.Where(i=>i.rodzic_id == key);
        this.OnRodzicsGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnRodzicsGet(ref IQueryable<Models.Baza.Rodzic> items);

    partial void OnRodzicDeleted(Models.Baza.Rodzic item);

    [HttpDelete("/odata/Baza/Rodzics(rodzic_id={rodzic_id})")]
    public IActionResult DeleteRodzic(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var itemToDelete = this.context.Rodzics
                .Where(i => i.rodzic_id == key)
                .Include(i => i.Uczens)
                .FirstOrDefault();

            if (itemToDelete == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            this.OnRodzicDeleted(itemToDelete);
            this.context.Rodzics.Remove(itemToDelete);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnRodzicUpdated(Models.Baza.Rodzic item);

    [HttpPut("/odata/Baza/Rodzics(rodzic_id={rodzic_id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutRodzic(int key, [FromBody]Models.Baza.Rodzic newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.rodzic_id != key))
            {
                return BadRequest();
            }

            this.OnRodzicUpdated(newItem);
            this.context.Rodzics.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Rodzics.Where(i => i.rodzic_id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Osoba");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/Baza/Rodzics(rodzic_id={rodzic_id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchRodzic(int key, [FromBody]Delta<Models.Baza.Rodzic> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemToUpdate = this.context.Rodzics.Where(i => i.rodzic_id == key).FirstOrDefault();

            if (itemToUpdate == null)
            {
                ModelState.AddModelError("", "Item no longer available");
                return BadRequest(ModelState);
            }

            patch.Patch(itemToUpdate);

            this.OnRodzicUpdated(itemToUpdate);
            this.context.Rodzics.Update(itemToUpdate);
            this.context.SaveChanges();

            var itemToReturn = this.context.Rodzics.Where(i => i.rodzic_id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Osoba");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnRodzicCreated(Models.Baza.Rodzic item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Baza.Rodzic item)
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

            this.OnRodzicCreated(item);
            this.context.Rodzics.Add(item);
            this.context.SaveChanges();

            var key = item.rodzic_id;

            var itemToReturn = this.context.Rodzics.Where(i => i.rodzic_id == key);

            Request.QueryString = Request.QueryString.Add("$expand", "Osoba");

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
