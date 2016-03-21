using System.Linq;
using AWModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace StoredProcedureTests
{
    
    
    /// <summary>
    ///This is a test class for Class1Test and is intended
    ///to contain all Class1Test Unit Tests
    ///</summary>
  [TestClass()]
  public class ContextTests
  {

    

    #region Additional test attributes
    // 
    //You can use the following additional attributes as you write your tests:
    //
    //Use ClassInitialize to run code before running the first test in the class
    //[ClassInitialize()]
    //public static void MyClassInitialize(TestContext testContext)
    //{
    //}
    //
    //Use ClassCleanup to run code after all tests in a class have run
    //[ClassCleanup()]
    //public static void MyClassCleanup()
    //{
    //}
    //
    //Use TestInitialize to run code before running each test
    //[TestInitialize()]
    //public void MyTestInitialize()
    //{
    //}
    //
    //Use TestCleanup to run code after each test has run
    //[TestCleanup()]
    //public void MyTestCleanup()
    //{
    //}
    //
    #endregion





    [TestMethod()]
    public void ModifyQuantityDatabaseIntegration()
    {
      var context = new AWEntities();
      var detail=context.SalesOrderDetails.First();
      detail.OrderQty += 1;
      context.SaveChanges();
      Assert.Inconclusive("the purpose of this test is to see profiler activity, assertions are not applicable");
    }

    [TestMethod()]
    public void ExecuteFunctionReturnsEntitiesImmediately()
    {
      var context = new AWEntities();
      var details = context.GetOrderDetailsForOrder(71796);
      Assert.AreEqual(2,details.Count());
      Assert.AreNotEqual(0,context.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Unchanged).Count());
      
    }

    [TestMethod()]
    public void FunctionReturnsSetOfStrings()
    {
      var context = new AWEntities();
      var details = context.GetCustomersWithOrdersGreaterThan(10000).ToList();
      Assert.AreNotEqual(0, details.Count());
      System.Diagnostics.Debug.WriteLine("# customers: {0}", details.Count);
      //test to assure that context is not tracking anything after this query
      Assert.AreEqual(0, context.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Unchanged).Count());

    }
    [TestMethod()]
    public void FunctionReturnsSetOfComplexTypes()
    {
      var context = new AWEntities();
      var details = context.GetCustomerNamesWithSalesOrderTotals().ToList();
      Assert.IsInstanceOfType(details[0], typeof(CustomerOrderOverview),"Function did not return CustomerOrderOverview objects");
      Assert.AreNotEqual(0, details.Count());
      //test to assure that context is not tracking anything after this query
      Assert.AreEqual(0, context.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Unchanged).Count());

    }
    [TestMethod()]
    public void FunctionReturnsRowsAffected()
    {
      var context = new AWEntities();
      Assert.AreEqual(1,context.ApplyShipDateToOrder(71796,new DateTime(2011,2,2)));
      Assert.AreEqual(new DateTime(2011, 2, 2), context.Orders.SingleOrDefault(o => o.SalesOrderID == 71796).ShipDate);
    }
  }
}
