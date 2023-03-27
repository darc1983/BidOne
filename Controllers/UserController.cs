using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BidOne.Helpers;
using BidOne.Models;

namespace MVCApp.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        List<User> users = GetUsers();
        return View(users);
    }

    public IActionResult Details(int? id)
    {
        if(!id.HasValue){
            return RedirectToAction("Index");
        }
        List<User> users = GetUsers();
        
        return View(users.FirstOrDefault(u => u.Id == id));
    }

    public IActionResult Create()
{

    return View();
}

    [HttpPost]  
    public IActionResult Create(User user)  
    { 
        try
        {
            List<User> users = GetUsers();
            user.Id = users.Count == 0 ? 1 : users.Max(u => u.Id) + 1;
            users.Add(user);
            JsonSerialization.WriteToJsonFile("users.json", users, false);
            TempData["GlobalMessage"] = "Success!";
            return RedirectToAction("Index");
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return View(user);
        }
          
    }

    [HttpPost]  
    public IActionResult Delete(int? id)  
    { 
        if(!id.HasValue){
            return RedirectToAction("Index");
        }
        try
        {
            List<User> users = GetUsers();
            users.RemoveAll(u => u.Id == id);
            JsonSerialization.WriteToJsonFile("users.json", users, false);
            TempData["GlobalMessage"] = "Success!";
            return RedirectToAction("Index");
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return RedirectToAction("Index");
        }
          
    }


    private List<User> GetUsers(){
        if(!System.IO.File.Exists("users.json"))
            return new List<User>();
        List<User>? users = JsonSerialization.ReadFromJsonFile<List<User>>("users.json");

        return users == null ? new List<User>() : users;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
