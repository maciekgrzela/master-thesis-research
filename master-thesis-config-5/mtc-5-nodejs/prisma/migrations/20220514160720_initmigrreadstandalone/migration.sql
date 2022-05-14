BEGIN TRY

BEGIN TRAN;

-- CreateTable
CREATE TABLE [dbo].[Bill] (
    [id] NVARCHAR(1000) NOT NULL,
    [orderId] NVARCHAR(1000) NOT NULL,
    [customerId] NVARCHAR(1000),
    [netPrice] DECIMAL(32,16) NOT NULL,
    [tax] DECIMAL(32,16) NOT NULL,
    [grossPrice] DECIMAL(32,16) NOT NULL,
    [created] DATETIME2 NOT NULL CONSTRAINT [Bill_created_df] DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT [Bill_pkey] PRIMARY KEY CLUSTERED ([id])
);

-- CreateTable
CREATE TABLE [dbo].[Course] (
    [id] NVARCHAR(1000) NOT NULL,
    [name] NVARCHAR(1000) NOT NULL,
    [grossPrice] DECIMAL(32,16) NOT NULL,
    [netPrice] DECIMAL(32,16) NOT NULL,
    [tax] INT NOT NULL,
    [preparationTimeInMinutes] INT NOT NULL,
    [coursesCategoryId] NVARCHAR(1000) NOT NULL,
    CONSTRAINT [Course_pkey] PRIMARY KEY CLUSTERED ([id])
);

-- CreateTable
CREATE TABLE [dbo].[Ingredient] (
    [id] NVARCHAR(1000) NOT NULL,
    [courseId] NVARCHAR(1000),
    [productId] NVARCHAR(1000) NOT NULL,
    [amount] DECIMAL(32,16) NOT NULL,
    CONSTRAINT [Ingredient_pkey] PRIMARY KEY CLUSTERED ([id])
);

-- CreateTable
CREATE TABLE [dbo].[CoursesCategory] (
    [id] NVARCHAR(1000) NOT NULL,
    [name] NVARCHAR(1000) NOT NULL,
    CONSTRAINT [CoursesCategory_pkey] PRIMARY KEY CLUSTERED ([id])
);

-- CreateTable
CREATE TABLE [dbo].[Customer] (
    [id] NVARCHAR(1000) NOT NULL,
    [name] NVARCHAR(1000) NOT NULL,
    [address1] NVARCHAR(1000) NOT NULL,
    [address2] NVARCHAR(1000) NOT NULL,
    [nip] NVARCHAR(1000) NOT NULL,
    CONSTRAINT [Customer_pkey] PRIMARY KEY CLUSTERED ([id])
);

-- CreateTable
CREATE TABLE [dbo].[Hall] (
    [id] NVARCHAR(1000) NOT NULL,
    [rowNumber] INT NOT NULL,
    [columnNumber] INT NOT NULL,
    [description] NVARCHAR(1000) NOT NULL,
    CONSTRAINT [Hall_pkey] PRIMARY KEY CLUSTERED ([id])
);

-- CreateTable
CREATE TABLE [dbo].[Table] (
    [id] NVARCHAR(1000) NOT NULL,
    [hallId] NVARCHAR(1000) NOT NULL,
    [startCoordinateX] INT NOT NULL,
    [startCoordinateY] INT NOT NULL,
    [endCoordinateX] INT NOT NULL,
    [endCoordinateY] INT NOT NULL,
    CONSTRAINT [Table_pkey] PRIMARY KEY CLUSTERED ([id])
);

-- CreateTable
CREATE TABLE [dbo].[Order] (
    [id] NVARCHAR(1000) NOT NULL,
    [tableId] NVARCHAR(1000) NOT NULL,
    [note] NVARCHAR(1000) NOT NULL,
    [userId] NVARCHAR(1000) NOT NULL,
    [created] DATETIME2 NOT NULL CONSTRAINT [Order_created_df] DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT [Order_pkey] PRIMARY KEY CLUSTERED ([id])
);

-- CreateTable
CREATE TABLE [dbo].[User] (
    [id] NVARCHAR(1000) NOT NULL,
    [firstName] NVARCHAR(1000) NOT NULL,
    [lastName] NVARCHAR(1000) NOT NULL,
    [userName] NVARCHAR(1000) NOT NULL,
    [email] NVARCHAR(1000) NOT NULL,
    [passwordHash] NVARCHAR(1000) NOT NULL,
    [phoneNumber] NVARCHAR(1000) NOT NULL,
    [twoFactorEnabled] BIT NOT NULL CONSTRAINT [User_twoFactorEnabled_df] DEFAULT 0,
    [emailConfirmed] BIT NOT NULL CONSTRAINT [User_emailConfirmed_df] DEFAULT 1,
    [normalizedUserName] NVARCHAR(1000) NOT NULL,
    [normalizedEmail] NVARCHAR(1000) NOT NULL,
    [lockoutEnd] DATETIME2,
    [lockoutEnabled] BIT NOT NULL CONSTRAINT [User_lockoutEnabled_df] DEFAULT 0,
    [accessFailedCount] INT NOT NULL CONSTRAINT [User_accessFailedCount_df] DEFAULT 0,
    [userRoleId] NVARCHAR(1000),
    CONSTRAINT [User_pkey] PRIMARY KEY CLUSTERED ([id]),
    CONSTRAINT [User_userName_key] UNIQUE NONCLUSTERED ([userName])
);

-- CreateTable
CREATE TABLE [dbo].[OrderedCourse] (
    [id] NVARCHAR(1000) NOT NULL,
    [courseId] NVARCHAR(1000) NOT NULL,
    [orderId] NVARCHAR(1000) NOT NULL,
    [billId] NVARCHAR(1000),
    [quantity] INT NOT NULL,
    [billQuantity] INT,
    [percentageDiscount] INT NOT NULL,
    CONSTRAINT [OrderedCourse_pkey] PRIMARY KEY CLUSTERED ([id])
);

-- CreateTable
CREATE TABLE [dbo].[Product] (
    [id] NVARCHAR(1000) NOT NULL,
    [name] NVARCHAR(1000) NOT NULL,
    [amount] DECIMAL(32,16) NOT NULL,
    [unit] NVARCHAR(1000) NOT NULL,
    [productsCategoryId] NVARCHAR(1000) NOT NULL,
    CONSTRAINT [Product_pkey] PRIMARY KEY CLUSTERED ([id])
);

-- CreateTable
CREATE TABLE [dbo].[ProductsCategory] (
    [id] NVARCHAR(1000) NOT NULL,
    [name] NVARCHAR(1000) NOT NULL,
    CONSTRAINT [ProductsCategory_pkey] PRIMARY KEY CLUSTERED ([id])
);

-- CreateTable
CREATE TABLE [dbo].[StatusEntry] (
    [id] NVARCHAR(1000) NOT NULL,
    [statusId] NVARCHAR(1000) NOT NULL,
    [orderId] NVARCHAR(1000),
    [orderedCourseId] NVARCHAR(1000),
    [created] DATETIME2 NOT NULL CONSTRAINT [StatusEntry_created_df] DEFAULT CURRENT_TIMESTAMP,
    [note] NVARCHAR(1000) NOT NULL,
    CONSTRAINT [StatusEntry_pkey] PRIMARY KEY CLUSTERED ([id])
);

-- CreateTable
CREATE TABLE [dbo].[Reservation] (
    [id] NVARCHAR(1000) NOT NULL,
    [tableId] NVARCHAR(1000) NOT NULL,
    [beginning] DATETIME2 NOT NULL,
    [ending] DATETIME2,
    CONSTRAINT [Reservation_pkey] PRIMARY KEY CLUSTERED ([id])
);

-- CreateTable
CREATE TABLE [dbo].[Status] (
    [id] NVARCHAR(1000) NOT NULL,
    [name] NVARCHAR(1000) NOT NULL,
    CONSTRAINT [Status_pkey] PRIMARY KEY CLUSTERED ([id])
);

-- CreateTable
CREATE TABLE [dbo].[Role] (
    [id] NVARCHAR(1000) NOT NULL,
    [name] NVARCHAR(1000) NOT NULL,
    CONSTRAINT [Role_pkey] PRIMARY KEY CLUSTERED ([id])
);

-- CreateTable
CREATE TABLE [dbo].[UserRole] (
    [id] NVARCHAR(1000) NOT NULL,
    [userId] NVARCHAR(1000) NOT NULL,
    [roleId] NVARCHAR(1000) NOT NULL,
    CONSTRAINT [UserRole_pkey] PRIMARY KEY CLUSTERED ([id]),
    CONSTRAINT [UserRole_userId_key] UNIQUE NONCLUSTERED ([userId])
);

-- AddForeignKey
ALTER TABLE [dbo].[Bill] ADD CONSTRAINT [Bill_customerId_fkey] FOREIGN KEY ([customerId]) REFERENCES [dbo].[Customer]([id]) ON DELETE SET NULL ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE [dbo].[Bill] ADD CONSTRAINT [Bill_orderId_fkey] FOREIGN KEY ([orderId]) REFERENCES [dbo].[Order]([id]) ON DELETE NO ACTION ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE [dbo].[Course] ADD CONSTRAINT [Course_coursesCategoryId_fkey] FOREIGN KEY ([coursesCategoryId]) REFERENCES [dbo].[CoursesCategory]([id]) ON DELETE NO ACTION ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE [dbo].[Ingredient] ADD CONSTRAINT [Ingredient_courseId_fkey] FOREIGN KEY ([courseId]) REFERENCES [dbo].[Course]([id]) ON DELETE SET NULL ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE [dbo].[Ingredient] ADD CONSTRAINT [Ingredient_productId_fkey] FOREIGN KEY ([productId]) REFERENCES [dbo].[Product]([id]) ON DELETE NO ACTION ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE [dbo].[Table] ADD CONSTRAINT [Table_hallId_fkey] FOREIGN KEY ([hallId]) REFERENCES [dbo].[Hall]([id]) ON DELETE NO ACTION ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE [dbo].[Order] ADD CONSTRAINT [Order_tableId_fkey] FOREIGN KEY ([tableId]) REFERENCES [dbo].[Table]([id]) ON DELETE NO ACTION ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE [dbo].[Order] ADD CONSTRAINT [Order_userId_fkey] FOREIGN KEY ([userId]) REFERENCES [dbo].[User]([id]) ON DELETE NO ACTION ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE [dbo].[OrderedCourse] ADD CONSTRAINT [OrderedCourse_billId_fkey] FOREIGN KEY ([billId]) REFERENCES [dbo].[Bill]([id]) ON DELETE SET NULL ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE [dbo].[OrderedCourse] ADD CONSTRAINT [OrderedCourse_courseId_fkey] FOREIGN KEY ([courseId]) REFERENCES [dbo].[Course]([id]) ON DELETE NO ACTION ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE [dbo].[OrderedCourse] ADD CONSTRAINT [OrderedCourse_orderId_fkey] FOREIGN KEY ([orderId]) REFERENCES [dbo].[Order]([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[Product] ADD CONSTRAINT [Product_productsCategoryId_fkey] FOREIGN KEY ([productsCategoryId]) REFERENCES [dbo].[ProductsCategory]([id]) ON DELETE NO ACTION ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE [dbo].[StatusEntry] ADD CONSTRAINT [StatusEntry_orderId_fkey] FOREIGN KEY ([orderId]) REFERENCES [dbo].[Order]([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[StatusEntry] ADD CONSTRAINT [StatusEntry_orderedCourseId_fkey] FOREIGN KEY ([orderedCourseId]) REFERENCES [dbo].[OrderedCourse]([id]) ON DELETE SET NULL ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE [dbo].[StatusEntry] ADD CONSTRAINT [StatusEntry_statusId_fkey] FOREIGN KEY ([statusId]) REFERENCES [dbo].[Status]([id]) ON DELETE NO ACTION ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE [dbo].[Reservation] ADD CONSTRAINT [Reservation_tableId_fkey] FOREIGN KEY ([tableId]) REFERENCES [dbo].[Table]([id]) ON DELETE NO ACTION ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE [dbo].[UserRole] ADD CONSTRAINT [UserRole_userId_fkey] FOREIGN KEY ([userId]) REFERENCES [dbo].[User]([id]) ON DELETE NO ACTION ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE [dbo].[UserRole] ADD CONSTRAINT [UserRole_roleId_fkey] FOREIGN KEY ([roleId]) REFERENCES [dbo].[Role]([id]) ON DELETE NO ACTION ON UPDATE CASCADE;

COMMIT TRAN;

END TRY
BEGIN CATCH

IF @@TRANCOUNT > 0
BEGIN
    ROLLBACK TRAN;
END;
THROW

END CATCH
