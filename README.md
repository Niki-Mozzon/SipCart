# SipCart
Wanna something to drink?

# How to run

  ## How to run the back end
  There are 2 ways to run the back end, one is through the exe (or pressing "Play" is VS) and the other one is through docker, let's see both:

  ### Run in IDE
  Go to `.\SipCart\SipCartBE\SipCart`.
  Click on `SipCart.sln`.
  Once the IDE is open we can press play.
  ![image](https://github.com/user-attachments/assets/6c8274e4-d717-43c5-b7e5-c6f93bc68180)
  Don't forget now to create the database, go to SQL Server, access your server and create a database called `app`, and then run this query to populate it:
      
    CREATE TABLE [drinks] (
      [Id] INT PRIMARY KEY IDENTITY (1,1),
      [Name] VARCHAR(50) NOT NULL,
      [Price] DECIMAL(18,2) NOT NULL,
    );

    CREATE TABLE [Coupons] (
      [Code] VARCHAR(10) PRIMARY KEY,
      [PercentageReduction] DECIMAL CHECK ([PercentageReduction] BETWEEN 0 AND 100) NOT NULL,
    );

    CREATE TABLE [orders] (
      [Id] INT PRIMARY KEY IDENTITY (1,1),
      [CouponCode]VARCHAR(10),
      [TotalPrice] DECIMAL(18,2) NOT NULL,
      [PaymentMethod] VARCHAR(4) NOT NULL,
      FOREIGN KEY ([CouponCode]) REFERENCES [Coupons]([Code])
    );

    -- Insert test data
    INSERT INTO [drinks] ([Name], [Price]) 
    VALUES 
      ( 'American Coffee',1.5),
      ( 'Italian Coffee',1.99),
      ( 'Chocolate',3),
      ( 'Tea',5)
    ;

    INSERT INTO [Coupons] ([Code], [PercentageReduction]) 
    VALUES 
      ('ABC', 50),
      ('XYZ', 20)
    ;
  Once we've done this don't forget to go to the `appsetting.Development.json`, press on the dropdown tree of `appsetting.json` if you cannot find it, and replace the 'Server'   address with yours.
  

  ### Run in docker
  With this method you don't need to congigure the db, but you must have docker installed, and it will take quite a few mins to download all the images if you already don't      have them.
  Open the terminal.
  Go to `.\SipCart\SipCartBE\SipCart`.
  Digit `docker-compose up` and press Enter.
  ![image](https://github.com/user-attachments/assets/4c7fb599-83f2-4861-ae1a-1d865f28c277)
  
  It will take a long time because it will download a SQL Server image, which will be filled with some data, then it will also automatically run the tests which will query   
  another SQL Server DB instance inside a container of a test-container.
  
  
  ## How to run the front end
  To run the app you need to have Angular installed (I'm using 14.2.13) and Node (I'm using 14.20.0).
  
  PS: If you want to run with the back end in docker you have to go to `.\SipCart\sipcart\src\environments`, open the `environment.ts`, comment the line 7 and uncomment line 8.

  Open the terminal.
  Go to `.\SipCart\sipcart`;
  Digit `npm i`, press Enter and wait the end of the process.
  Digit `ng server`, press Enter and wait the end of the process
  Open any browser you want and go to `http://localhost:4200/drinks`.
  At this point the application will work.

  ## Testing
  Intregration Tests (the ones that end with (*ControllerTest) must be run on Docker, since there is an integration database containerized, so make sure to have docker     
  installed and runnning on the background.
  Tests can be run while the app is deployed with `docker compose -p tests up` or by IDE right clicking on the `SipCartTesting` project and click on `Run Tests`


  # The app
  Once the DB is created, the back end is runnning and the front end too, we can finally access our website..
  ### Landing page
  When we land to http://localhost:4200/ we will see this page:
  ![image](https://github.com/user-attachments/assets/6ef8ff3b-bdba-4439-ab63-4a21b2baf608)
  Allows the user to select the drinks he wants to order and set the quantity.

  ### Cart
  When clicking on the cart icon in the top right of the screen we will open the cart:
  ![image](https://github.com/user-attachments/assets/2d207bc7-99d7-4997-92b8-88924bd5054a)
  Displays the products in the cart and set the quantity of the items in the cart.

  ### Coupons
  When the cart has at least one item and we press on Checkout message will pop up:
  ![image](https://github.com/user-attachments/assets/2bfea2b3-882d-4c83-969f-5b44786cb5d1)
  Where we can input a wrong coupon code and retry, insert a correct one and proceed further or just skip the coupon insertion and go directly to the checkout page (enter "abc" to test the coupon)

  ### Checkout
  Once we proceed further from the coupon pop up we arrive to the check out
  ![image](https://github.com/user-attachments/assets/e039f8f8-dc3f-44dd-ba93-b66d5ffdffe5)
  Here we can review our order and select the payment method we want (of course the amounts greater than 10€ will force the user to use card payments).

  ### End
  When we will purchsa the good we will see a message with the id of the order created in the database:
  ![image](https://github.com/user-attachments/assets/b54c23e1-78e4-43f3-bafe-9e5be26cdbcf)


  
  # Flow
  ![diagram](https://github.com/user-attachments/assets/8aedbb3d-969d-4d39-81ae-e3ccce7661a8)

  # Possible problems

  ## With Docker
  If there are problems with DB in Docker make sure the entrypoint.sh fila is saved in LF
  
  ![image](https://github.com/user-attachments/assets/4ad467c1-6de0-4a24-903b-c4260a2a36e8)

  Also in Docker make sure to call the api after you the db is populated
  
  ![image](https://github.com/user-attachments/assets/864f0857-e1a5-4244-a8bb-16f4b353c0d1)

  


