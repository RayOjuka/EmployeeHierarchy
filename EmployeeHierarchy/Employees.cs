using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace EmployeeHierarchy
{
    public class Employees
    {
        // Read the contents of the csv file as an individual line
        

        public Employees(string csv)
        {
            string[] csvLines = File.ReadAllLines(csv);

            var employeeIds = new List<string>();
            var managerIds = new List<string>();
            var employeesSalary = new List<int>();

            // Split each row into column data
            for (int i = 1; i < csvLines.Length; i++)
            {
                string[] rowData = csvLines[i].Split(',');

                // Validates salaries to be valid integer numbers
                string salary = rowData[2];
                int number;
                if (!Int32.TryParse(salary, out number))
                {
                    throw new Exception("Employees salaries must be a valid integer.");
                }
                employeesSalary.Add(Convert.ToInt32(salary));


            }

            // Check for Circular reference 
            for (var i = 0; i < employeeIds.Count; i++)
            {
                string[] rowData = csvLines[i].Split(',');
                var employeeData = employeeIds[i];
                var employeeManager = rowData[1];
                int index = employeeData.IndexOf(employeeManager);

                var managerData = employeeIds[index];
                var topManager = managerData[1];

                if (index != -1)
                {
                    if (employeeManager.Contains(topManager))
                    {
                        throw new Exception("Circular reference error");
                    }
                }

                // Validates One employee does not report to more than one manager               
                if (employeeData.Contains(employeeManager))
                {
                    throw new Exception("There is a possibility of an employee reporting to more than one manager in the file.");
                }

                // Check if all managers are employees
                if (!employeeManager.Contains(employeeData))
                {
                    throw new Exception("There are some managers who are not listed an employees");
                }

                // check if we only have one CEO
                int ceo = Convert.ToInt32(rowData[0]) - Convert.ToInt32(rowData[1]);
                if (ceo != 1)
                {
                    throw new Exception("There might be more than one CEO in the csv file");
                }
                employeeIds.Add(rowData[0]);
                managerIds.Add(rowData[1]);
            }
        }

        // Return salary budgets of a specified manager
        public long ManagerSalaryBudget(string csv)
        {
            string[] csvLines = File.ReadAllLines(csv);
            var employeesSalary = new List<int>();
            long totalManagerSalary = 0;
            for (int i = 1; i < employeesSalary.Count; i++)
            {
                string[] rowData = csvLines[i].Split(',');
                var employee = rowData[0];
                var manager = rowData[1];
                var salary = rowData[2];
                if (manager == csv || employee == csv)
                {
                    totalManagerSalary += Convert.ToInt64(salary);
                }
            }
            return totalManagerSalary;
        }
    }
}
