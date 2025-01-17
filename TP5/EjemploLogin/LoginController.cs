// public class LoginController 
// {
//     private readonly InMemoryUserRepository _inMemoryUserRepository; 
//     private ILogger<LogginController> _logger;

//     public LoginController(InMemoryUserRepository InMemoryUserRepository)
//     {
//         InMemoryUserRepository = _inMemoryUserRepository; 
//     }

//     public IActionResult Index()
//     {
//         var model = new LoginViewModel
//         {
//             IsAuthenticated = HttpContext.Session.GetString("IsAuthenticated") == "true" 
//         }; 
//         return View(model); 
//     }

//     public IActionResult Login (LoginViewModel model)
//     {
//         if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
//         User usuario = InMemoryUserRepository.GetUser(model.Username, model.Password); 
//         if (usuario!=null)
//         {
//             HttpContext.Session.SetString("IsAuthenticated", "true"); 
//             HttpContext.Session.SetString("User", username); 
//             HttpContext.Session.SetString("IsAuthenticated", usuario.AccessLevel.ToString(); )
//             return RedirectToAction("Index", "Home"); 
//         }

//         model.ErrorMessage = "Ingresaste cualqueir cosa"; 
//         model.IsAuthenticated = false; 
//         return View("Index", model); 
//     }

//     //  if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
//     //     {
//     //         model.ErrorMessage = "Por favor ingrese su nombre de usuario y contraseña.";
//     //         return View("Index", model);
//     //     }


    // public IActionResult Logout()
    // {
    //     // Limpiar la sesión
    //     HttpContext.Session.Clear();

    //     // Redirigir a la vista de login
    //     return RedirectToAction("Index");
    // }

// }