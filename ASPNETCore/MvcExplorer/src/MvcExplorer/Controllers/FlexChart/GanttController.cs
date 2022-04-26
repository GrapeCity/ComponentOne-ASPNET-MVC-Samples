using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Task = MvcExplorer.Models.Task;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController
    {
        public IActionResult Gantt()
        {
            return View(GetTasks());
        }

        private IEnumerable<Task> GetTasks()
        {
            var list = new List<Task>();
            list.Add(new Task { Name = "Task1", Start = new DateTime(2017, 1, 1), End = new DateTime(2017, 3, 31), Parent = null, Percent = 100 });
            list.Add(new Task { Name = "Task2", Start = new DateTime(2017, 4, 1), End = new DateTime(2017, 4, 30), Parent = "Task1", Percent = 100 });
            list.Add(new Task { Name = "Task3", Start = new DateTime(2017, 5, 1), End = new DateTime(2017, 7, 31), Parent = "Task2", Percent = 75 });
            list.Add(new Task { Name = "Task4", Start = new DateTime(2017, 4, 1), End = new DateTime(2017, 7, 31), Parent = "Task1", Percent = 33 });
            list.Add(new Task { Name = "Task5", Start = new DateTime(2017, 8, 1), End = new DateTime(2017, 9, 30), Parent = "Task3,Task4", Percent = 0 });
            list.Add(new Task { Name = "Task6", Start = new DateTime(2017, 10, 1), End = new DateTime(2017, 12, 31), Parent = "Task1,Task5", Percent = 0 });
            list.Add(new Task { Name = "Task7", Start = new DateTime(2017, 1, 1), End = new DateTime(2017, 12, 31), Parent = null, Percent = 50 });
            return list;
        }

    }
}
