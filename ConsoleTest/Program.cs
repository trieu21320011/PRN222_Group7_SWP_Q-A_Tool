using BussinessLayer.IRepositories;
using BussinessLayer.Repositories;
using DataLayer.DataLayer;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("=== SWP QA Tool - Console Test ===\n");

// Create DbContext
using var context = new SWP391QAContext();

// Create UserRepository
IUserRepository userRepository = new UserRepository(context);

try
{
    // Test 1: Get all users
    Console.WriteLine("1. Getting all users...");
    var allUsers = await userRepository.GetAllAsync();
    Console.WriteLine($"   Found {allUsers.Count()} users:");
    foreach (var user in allUsers.Take(10)) // Limit to first 10
    {
        Console.WriteLine($"   - [{user.UserId}] {user.FullName} ({user.Email}) - RoleId: {user.RoleId}");
    }
    if (allUsers.Count() > 10)
        Console.WriteLine($"   ... and {allUsers.Count() - 10} more users");

    Console.WriteLine();

    // Test 2: Get active users
    Console.WriteLine("2. Getting active users...");
    var activeUsers = await userRepository.GetActiveUsersAsync();
    Console.WriteLine($"   Found {activeUsers.Count()} active users");

    Console.WriteLine();

    // Test 3: Get users by role (RoleId = 1)
    Console.WriteLine("3. Getting users by RoleId = 1...");
    var usersByRole = await userRepository.GetUsersByRoleAsync(1);
    Console.WriteLine($"   Found {usersByRole.Count()} users with RoleId = 1");

    Console.WriteLine();

    // Test 4: Get user by email (if exists)
    if (allUsers.Any())
    {
        var firstEmail = allUsers.First().Email;
        Console.WriteLine($"4. Getting user by email: {firstEmail}...");
        var userByEmail = await userRepository.GetUserByEmailAsync(firstEmail);
        if (userByEmail != null)
        {
            Console.WriteLine($"   Found: {userByEmail.FullName}");
        }
    }

    Console.WriteLine();

    // Test 5: Get user with role included
    if (allUsers.Any())
    {
        var firstUserId = allUsers.First().UserId;
        Console.WriteLine($"5. Getting user with role (UserId = {firstUserId})...");
        var userWithRole = await userRepository.GetUserWithRoleAsync(firstUserId);
        if (userWithRole != null)
        {
            Console.WriteLine($"   User: {userWithRole.FullName}");
            Console.WriteLine($"   Role: {userWithRole.Role?.RoleName ?? "N/A"}");
        }
    }

    Console.WriteLine("\n=== All tests completed successfully! ===");
}
catch (Exception ex)
{
    Console.WriteLine($"\nError: {ex.Message}");
    Console.WriteLine($"Stack: {ex.StackTrace}");
}

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();
