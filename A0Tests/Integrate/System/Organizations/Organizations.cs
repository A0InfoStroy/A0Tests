// $Date: 2020-08-06 17:55:16 +0300 (Чт, 06 авг 2020) $
// $Revision: 352 $
// $Author: agalkin $
// Тесты Организаций

namespace A0Tests.Integrate.Sys
{
    using System;
    using System.Data.OleDb;
    using A0Service;
    using ADODB;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности каталога организаций.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0SysOrganizationsRepo",
        Author = "agalkin")]
    public class Test_IA0SysOrganizationsRepo : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает каталог организаций.
        /// </summary>
        protected IA0SysOrganizationsRepo Repo { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Repo = this.A0.Sys.Organizations.Repo;
            Assert.NotNull(this.Repo);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Repo);
            this.Repo = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет работоспособность чтения данных структур организаций.
        /// </summary>
        [Test(Description = "Структуры организаций")]
        public void Test_TreeID()
        {
            // Поля для запроса.
            IAppFieldsReq fields = this.Repo.TreeID.GetFieldRequest();
            fields.Add("Mark");
            fields.Add("TreeName");
            fields.Add("CreateMoment");
            fields.Add("UpdateMoment");

            IA0FieldsIterator iter = this.Repo.TreeID.Read(fields, null, null);
            Assert.NotNull(iter);
            int count = iter.Count;
            Assert.NotZero(count, "Ожидается наличие структур организаций в БД");

            while (!iter.EOF)
            {
                IFields currentItem = iter.Current;
                Assert.NotZero(currentItem.Count, "Ожидается наличие данных");
                int treeID = currentItem.Value["TreeID"];
                string mark = currentItem.Value["Mark"];
                Assert.NotNull(mark);
                string treeName = currentItem.Value["TreeName"];
                Assert.NotNull(treeName);
                DateTime createMoment = currentItem.Value["CreateMoment"];
                DateTime updateMoment = currentItem.Value["UpdateMoment"];
                iter.Next();
                count--;
            }

            Assert.Zero(count, "Выбраны не все данные");
        }

        /// <summary>
        /// Проверяет работоспособность чтения данных узлов структур организаций.
        /// </summary>
        [Test(Description = "Узлы структур организаций")]
        public void Test_TreeNodeID()
        {
            // Поля для запроса
            var fields = this.Repo.TreeNodeID.GetFieldRequest();
            fields.Add("NodeMark");
            fields.Add("NodeName");
            fields.Add("NodeType");

            // Итератор по структурам организаций.
            IA0FieldsIterator iterTree = this.Repo.TreeID.Read(null, null, null);
            Assert.NotNull(iterTree);
            Assert.NotZero(iterTree.Count, "Ожидается наличие структур организаций в БД");

            while (!iterTree.EOF)
            {
                IFields treeFields = iterTree.Current;
                int treeID = treeFields.Value["TreeID"];

                // Итератор по узлам.
                IA0FieldsIterator iterNode = this.Repo.TreeNodeID.Read(treeID, fields, null, null);

                int count = iterNode.Count;

                while (!iterNode.EOF)
                {
                    IFields nodeFields = iterNode.Current;

                    Assert.NotZero(nodeFields.Count, "Ожидается наличие данных");

                    int nodeTreeID = nodeFields.Value["TreeID"];
                    Assert.AreEqual(treeID, nodeTreeID);

                    int parentID = nodeFields.Value["ParentID"];
                    int nodeID = nodeFields.Value["NodeID"];

                    string nodeMark = nodeFields.Value["NodeMark"];
                    Assert.NotNull(nodeMark);

                    string nodeName = nodeFields.Value["NodeName"];
                    Assert.NotNull(nodeName);

                    EExecutorsTreeNodeType nodeType = (EExecutorsTreeNodeType)nodeFields.Value["NodeType"];

                    iterNode.Next();
                    count--;
                }

                Assert.Zero(count, "Выбраны не все данные");

                iterTree.Next();
            }
        }

        /// <summary>
        /// Проверяет работоспособность чтения данных исполнителей.
        /// </summary>
        [Test(Description = "Исполнители")]
        public void Test_ExecutorID()
        {
            // Поля для запроса.
            IAppFieldsReq fields = this.Repo.ExecutorID.GetFieldRequest();
            fields.Add("INN");
            fields.Add("OKPO");
            fields.Add("Shifr");
            fields.Add("FullName");
            fields.Add("ShortName");
            fields.Add("OKDP");
            fields.Add("WorkType");
            fields.Add("Country");
            fields.Add("ZipCode");
            fields.Add("Area");
            fields.Add("City");
            fields.Add("Street");
            fields.Add("HouseNumber");
            fields.Add("Box");
            fields.Add("Appartament");
            fields.Add("Phone");
            fields.Add("Fax");
            fields.Add("EMail");
            fields.Add("Notes");
            fields.Add("Executor.CreateMoment");
            fields.Add("Executor.UpdateMoment");
            fields.Add("KPP");

            // Итератор по исполнителя.
            IA0FieldsIterator iterExecutor = this.Repo.ExecutorID.Read(fields, null, null);

            int count = iterExecutor.Count;

            while (!iterExecutor.EOF)
            {
                IFields executorFields = iterExecutor.Current;

                Assert.NotZero(executorFields.Count, "Ожидается наличие данных");

                int executorTreeID = executorFields.Value["TreeID"];

                int parentID = executorFields.Value["ParentID"];
                int nodeID = executorFields.Value["NodeID"];
                int executorID = executorFields.Value["ExecutorID"];
                Guid executorGUID = Guid.Parse(executorFields.Value["ExecutorGUID"]);

                string inn = executorFields.Value["INN"];
                string okpo = executorFields.Value["OKPO"];
                string shifr = executorFields.Value["Shifr"];
                string fullName = executorFields.Value["FullName"];
                string shortName = executorFields.Value["ShortName"];
                string okdp = executorFields.Value["OKDP"];
                string workType = executorFields.Value["WorkType"];
                string country = executorFields.Value["Country"];
                string sipCode = executorFields.Value["ZipCode"];
                string area = executorFields.Value["Area"];
                string city = executorFields.Value["City"];
                string street = executorFields.Value["Street"];
                string houseNumber = executorFields.Value["HouseNumber"];
                string box = executorFields.Value["Box"];
                string appartament = executorFields.Value["Appartament"];
                string phone = executorFields.Value["Phone"];
                string fax = executorFields.Value["Fax"];
                string eMail = executorFields.Value["EMail"];
                var notes = executorFields.Value["Notes"];
                DateTime createMoment = executorFields.Value["CreateMoment"];
                DateTime updateMoment = executorFields.Value["UpdateMoment"];
                string kpp = executorFields.Value["KPP"];

                iterExecutor.Next();
                count--;
            }

            Assert.Zero(count, "Выбраны не все данные");
        }

        /// <summary>
        /// Проверяет работоспособность чтения данных сотрудников.
        /// </summary>
        [Test(Description = "Сотрудники")]
        public void Test_EmployeeID()
        {
            // Поля для запроса.
            IAppFieldsReq fields = this.Repo.EmployeeID.GetFieldRequest();
            fields.Add("FIO");
            fields.Add("POST");
            fields.Add("Document");

            // Итератор	по сотрудникам.
            IA0FieldsIterator iterEmployee = this.Repo.EmployeeID.Read(fields, null, null);

            int count = iterEmployee.Count;

            while (!iterEmployee.EOF)
            {
                IFields employeeFields = iterEmployee.Current;

                Assert.NotZero(employeeFields.Count, "Ожидается наличие данных");

                int employeeTreeID = employeeFields.Value["TreeID"];
                int parentID = employeeFields.Value["ParentID"];
                int hodeID = employeeFields.Value["NodeID"];
                int executorID = employeeFields.Value["ExecutorID"];
                Guid executorGUID = Guid.Parse(employeeFields.Value["ExecutorGUID"]);
                int employeeID = employeeFields.Value["EmployeeID"];
                string fio = employeeFields.Value["FIO"];
                string post = employeeFields.Value["POST"];
                string document = employeeFields.Value["Document"];

                iterEmployee.Next();
                count--;
            }

            Assert.Zero(count, "Выбраны не все данные");
        }

        /// <summary>
        /// Проверяет работоспособность чтения данных дерева структуры организаций.
        /// </summary>
        [Test(Description = "Структуры организаций")]
        public void Test_Tree()
        {
            // Поля для запроса
            IAppFieldsReq reqFieldsTree = this.Repo.TreeID.GetFieldRequest();
            reqFieldsTree.Add("Mark");
            reqFieldsTree.Add("TreeName");
            reqFieldsTree.Add("CreateMoment");
            reqFieldsTree.Add("UpdateMoment");

            // Итератор по структурам организаций.
            IA0FieldsIterator iterTree = this.Repo.TreeID.Read(reqFieldsTree, null, null);
            Assert.NotNull(iterTree);
            Assert.NotZero(iterTree.Count, "Ожидается наличие структур организаций в БД");

            while (!iterTree.EOF)
            {
                IFields treeFields = iterTree.Current;
                int treeID = treeFields.Value["TreeID"];

                // Загрузка струтуры организации.
                IA0SysOrgTree tree = this.Repo.Tree.Read(treeID, EAccessKind.akRead);
                Assert.NotNull(tree);

                int id = tree.ID;
                Assert.AreEqual(id, treeFields.Value["TreeID"]);

                string mark = tree.Mark;
                Assert.AreEqual(mark, treeFields.Value["Mark"]);

                string name = tree.Name;
                Assert.AreEqual(name, treeFields.Value["TreeName"]);

                DateTime createMoment = tree.CreateMoment;
                Assert.AreEqual(createMoment, treeFields.Value["CreateMoment"]);

                DateTime updateMoment = tree.UpdateMoment;
                Assert.AreEqual(updateMoment, treeFields.Value["UpdateMoment"]);

                IA0SysOrgTreeNode headNode = tree.HeadNode;
                Assert.NotNull(headNode);

                // Загрузка узлов.
                // Поля для запроса узлов.
                IAppFieldsReq fields = this.Repo.TreeNodeID.GetFieldRequest();
                fields.Add("NodeMark");
                fields.Add("NodeName");
                fields.Add("NodeType");

                ISQLOrder order = this.Repo.TreeNodeID.GetOrderRequest();
                order.Clear();
                order.Add("[ExecutorsTreeNode].NodeID");

                ISQLWhere where = this.Repo.TreeNodeID.GetWhereRequest();

                where.Node.And3("[ExecutorsTreeNode].TreeID", "=", tree.ID.ToString());
                where.Node.And3("[ExecutorsTreeNode].ParentID", "=", tree.HeadNode.ID.ToString());

                // Итератор по узлам.
                IA0FieldsIterator iterNode = this.Repo.TreeNodeID.Read(treeID, fields, where, order);

                Assert.AreEqual(headNode.Nodes.Count, iterNode.Count);

                while (!iterNode.EOF)
                {
                    IFields fieldsNode = iterNode.Current;
                    Assert.NotNull(fieldsNode);
                    iterNode.Next();
                }

                iterTree.Next();
            }
        }
    }

    /// <summary>
    ///  Содержит тесты проверки работоспособности сервиса домена организаций.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0SysOrganizationsServices",
        Author = "agalkin")]
    public class Test_IA0SysOrganizationsServices : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает сервис домена организаций.
        /// </summary>
        protected IA0SysOrganizationsServices Services { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Services = this.A0.Sys.Organizations.Services;
            Assert.NotNull(this.Services);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Services);
            this.Services = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет работоспособность метода получения отсутствующего исполнителя по краткому наименованию.
        /// </summary>
        [Test(Description = "ИД отсутствующего исполнителя по краткому наименованию")]
        public void Test_ExecutorIDByShortNameNone()
        {
            int id = this.Services.ExecutorIDByShortName("Такого исполнителя нет в БД " + Guid.NewGuid().ToString());
            Assert.AreEqual(0, id);
        }

        /// <summary>
        /// Проверяет работоспособность метода получения исполнителя по краткому наименованию.
        /// </summary>
        [Test(Description = "ИД исполнителя по краткому наименованию")]
        public void Test_ExecutorIDByShortName()
        {
            // Получаем из таблицы БД ИД исполнителя и краткое наименование.
            // Отправляем запрос в A0Service.
            // Сравниваем результат.
            // Тест может не проходить, если краткое наименование не уникальное в системе.

            // Соединение с БД из A0Service
            _Connection connection = this.A0.App.Connection.MainConnection as _Connection;

            // Соедиение с БД для получения данных в обход A0Service.
            using (OleDbConnection oleDbConnection = new OleDbConnection(connection.ConnectionString))
            {
                oleDbConnection.Open();

                using (OleDbCommand command = new OleDbCommand("SELECT ExecutorID, ShortName FROM Executor", oleDbConnection))
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    Assert.True(reader.HasRows, "В системе ожидается наличие исполнителей");

                    while (reader.Read())
                    {
                        int executorID = reader.GetInt32(0);
                        string shortName = reader.GetString(1);

                        // Отправляем запрос в A0Service
                        int id = this.Services.ExecutorIDByShortName(shortName);
                        Assert.AreEqual(executorID, id);
                    }
                }
            }
        }
    }
}