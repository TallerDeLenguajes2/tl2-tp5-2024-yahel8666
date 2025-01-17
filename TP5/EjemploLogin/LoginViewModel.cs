// public class LoginViewModel
// {
//     public string Username { get; set; }
//     public string Password { get; set; }
//     public string ErrorMessage { get; set; }
//     public bool IsAuthenticated { get; set; } 
// }
 
// @model LoginViewModel  

// @{
//     ViewData["Title"] = "Login";
// }

// <h2 class="text-center">@ViewData["Title"]</h2>

// <!-- Mostrar mensaje de error si existe -->
// @if (!string.IsNullOrEmpty(Model.ErrorMessage))
// {
//     <div class="alert alert-danger" role="alert">
//         @Model.ErrorMessage
//     </div>
// }

// <div class="container d-flex justify-content-center align-items-center" style="min-height: 100vh;">
//     <div class="card shadow-sm p-4" style="max-width: 400px; width: 100%;">
//         <h4 class="card-title text-center mb-4">Iniciar sesión</h4>
//         <form method="post" asp-action="Login">
//             <div class="mb-3">
//                 <label for="username" class="form-label">Usuario:</label>
//                 <input type="text" class="form-control" id="username" name="username" value="@Model.Username" required />
//             </div>

//             <div class="mb-3">
//                 <label for="password" class="form-label">Contraseña:</label>
//                 <input type="password" class="form-control" id="password" name="password" required />
//             </div>

//             <div class="d-grid gap-2">
//                 <button type="submit" class="btn btn-primary">Iniciar sesión</button>
//             </div>
//         </form>
//     </div>
// </div>

