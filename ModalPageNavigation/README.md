
http://stackoverflow.com/questions/37398962/remove-modal-page-from-navigationstack

Here's an example for my `Navigation`:

    LoginPage ( Login  ) -> MainPage   |Block BackButton
    MainPage  ( Logout ) -> LoginPage  |Block going back to the MainPage

I am using

     await Navigation.PushModalAsync(new MainPage());
to open the MainPage after a successful login. I DONT want the users to go back by hitting the `PreviousButton(Android)` to return to the LoginPage.

Same story when logging out. 

Is there a way to remove the LoginPage from the NavigationStack after a successful login ?

**NOTE:** This is modal. I am not using a NavigationPage

-------

You are looking for `PopToRootAsync`. So your user enters required info and they tap a login button, you perform your login verification and if success you set a new `MainPage` and then `PopToRootAsync` which pops all but the root Page off the navigation stack.

**Update**: Due to the way `PopToRootAsync` is done across the various platforms, you need to start from a `NavigationPage` but can remove it as your root page after your login process. 

So in your Application constructor, instead of just creating your `LoginPage`, place it into a `NavigationPage` but **hide the navigation bar** so it does not effect your `LoginPage` screen layout:

	public App()
	{
		var navPage = new NavigationPage(new LoginPage());
		NavigationPage.SetHasNavigationBar(navPage.CurrentPage, false);
		MainPage = navPage;
	}

Then within your `LoginPage` you can set the `Application.Current.MainPage` to any `Page` class (does **not** to be a `NavigationPage`) and then `PopToRootAsync` to get to it and totally remove your `LoginPage` from the navigation hierarchy.

	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
			loginDone.Clicked += OnLoginClick;
		}
		async void OnLoginClick(object sender, EventArgs e)
		{
			// If Login is complete/successful - set new root page
            if (YourLoginMethod()) {
   			    Application.Current.MainPage = new MainApplicationPage();
			    // Pops all but the root Page off the navigation stack, with optional animation.
			    await Navigation.PopToRootAsync(true);
            }
		}
	}

Note: Tested this technique only on `iOS` and `Android`
