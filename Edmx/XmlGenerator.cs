using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Edmx
{
    internal class XmlGenerator
    {
        private readonly string _myDocument = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private readonly string _columns = Path.Combine($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}", "csv", "columns2.csv");
        private readonly string _constraint = Path.Combine($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}", "csv", "constaints2.csv");

        public async Task GenerateColumnAsync()
        {
            var content = await File.ReadAllLinesAsync(_columns);
            var r = content.Skip(1)
                .Select(s => s.Split(','))
                .Select(split => new Database
                {
                    TableName = split[0],
                    ColumnName = split[1],
                    Type = split[2],
                    Length = split[3],
                    Precision = split[4],
                    Scale = split[5],
                    Nullable = split[6].Equals("Y") ? "true" : "false"
                })
                .GroupBy(g => g.TableName)
                .ToList();

            var xElement = new XElement("Root");
            foreach (var a in r)
            {
                var t = new XElement("EntityType",
                    new XAttribute("Name", a.Key),
                    new XElement("Key",
                        new XElement("PropertyRef",
                            new XAttribute("Name", ""))));

                foreach (var database in a)
                {
                    switch (database.Type)
                    {
                        case "VARCHAR2":
                            {
                                t.Add(new XElement("Property",
                                    new XAttribute("Name", database.ColumnName),
                                    new XAttribute("Type", database.Type.ToLower()),
                                    new XAttribute("MaxLength", database.Length),
                                    new XAttribute("Nullable", database.Nullable)));
                                break;
                            }
                        case "NUMBER":
                            {
                                t.Add(new XElement("Property",
                                    new XAttribute("Name", database.ColumnName),
                                    new XAttribute("Type", database.Type.ToLower()),
                                    new XAttribute("Precision", database.Precision),
                                    new XAttribute("Scale", database.Scale),
                                    new XAttribute("Nullable", database.Nullable)));
                                break;
                            }
                        case "DATE":
                            {
                                t.Add(new XElement("Property",
                                    new XAttribute("Name", database.ColumnName),
                                    new XAttribute("Type", database.Type.ToLower()),
                                    new XAttribute("Nullable", database.Nullable)));
                                break;
                            }
                    }
                }
                xElement.Add(t);
            }

            var xml = Path.Combine($"{_myDocument}", "csv", "1.txt");
            xElement.Save(xml);
        }

        public async Task GenerateColumnAsync2()
        {
            var content = await File.ReadAllLinesAsync(_columns);
            var r = content.Skip(1)
                .Select(s => s.Split(','))
                .Select(split => new Database
                {
                    TableName = split[0],
                    ColumnName = split[1],
                    Type = split[2],
                    Length = split[3],
                    Precision = split[4],
                    Scale = split[5],
                    Nullable = split[6].Equals("Y") ? "true" : "false"
                })
                .GroupBy(g => g.TableName)
                .ToList();

            var xElement = new XElement("Root");
            foreach (var a in r)
            {
                xElement.Add(new XElement("EntitySet",
                    new XAttribute("Name", a.Key),
                    new XAttribute("EntityType", $"Self.{a.Key}"),
                    new XAttribute("storeType", "Tables")));
            }

            var xml = Path.Combine($"{_myDocument}", "csv", "2.txt");
            xElement.Save(xml);
        }

        public async Task GenerateColumnAsync3()
        {
            var content = await File.ReadAllLinesAsync(_columns);
            var r = content.Skip(1)
                .Select(s => s.Split(','))
                .Select(split => new Database
                {
                    TableName = split[0],
                    ColumnName = split[1],
                    Type = split[2],
                    Length = split[3],
                    Precision = split[4],
                    Scale = split[5],
                    Nullable = split[6].Equals("Y") ? "true" : "false"
                })
                .GroupBy(g => g.TableName)
                .ToList();

            var xElement = new XElement("Root");
            foreach (var a in r)
            {
                var t = new XElement("EntityType",
                    new XAttribute("Name", a.Key),
                    new XElement("Key",
                        new XElement("PropertyRef",
                            new XAttribute("Name", ""))));

                foreach (var database in a)
                {
                    switch (database.Type)
                    {
                        case "VARCHAR2":
                            {
                                t.Add(new XElement("Property",
                                    new XAttribute("Name", database.ColumnName),
                                    new XAttribute("Type", "String"),
                                    new XAttribute("MaxLength", database.Length),
                                    new XAttribute("Nullable", database.Nullable),
                                    new XAttribute("FixedLength", "false"),
                                    new XAttribute("Unicode", "false")));
                                break;
                            }
                        case "NUMBER":
                            {
                                t.Add(new XElement("Property",
                                    new XAttribute("Name", database.ColumnName),
                                    new XAttribute("Type", "Int32"),
                                    new XAttribute("Nullable", database.Nullable)));
                                break;
                            }
                        case "DATE":
                            {
                                t.Add(new XElement("Property",
                                    new XAttribute("Name", database.ColumnName),
                                    new XAttribute("Type", "DateTime"),
                                    new XAttribute("Nullable", database.Nullable)));
                                break;
                            }
                    }
                }
                xElement.Add(t);
            }

            var xml = Path.Combine($"{_myDocument}", "csv", "3.txt");
            xElement.Save(xml);
        }

        public async Task GenerateColumnAsync4()
        {
            var content = await File.ReadAllLinesAsync(_columns);
            var r = content.Skip(1)
                .Select(s => s.Split(','))
                .Select(split => new Database
                {
                    TableName = split[0],
                    ColumnName = split[1],
                    Type = split[2],
                    Length = split[3],
                    Precision = split[4],
                    Scale = split[5],
                    Nullable = split[6].Equals("Y") ? "true" : "false"
                })
                .GroupBy(g => g.TableName)
                .ToList();

            var xElement = new XElement("Root");
            foreach (var a in r)
            {
                var t = new XElement("EntitySetMapping",
                    new XAttribute("Name", a.Key));

                var em = new XElement("EntityTypeMapping",
                    new XAttribute("TypeName", $"MTOA.DAL.EF.DatabaseContext.{a.Key}"));
                
                var m = new XElement("MappingFragment",
                    new XAttribute("StoreEntitySet", a.Key));

                foreach (var database in a)
                {
                    m.Add(new XElement("ScalarProperty",
                        new XAttribute("Name", database.ColumnName),
                        new XAttribute("ColumnName", database.ColumnName)));
                }

                t.Add(em);
                em.Add(m);
                xElement.Add(t);
            }

            var xml = Path.Combine($"{_myDocument}", "csv", "4.txt");
            xElement.Save(xml);
        }

        public async Task GenerateConstraintsAsync()
        {
            var content = await File.ReadAllLinesAsync(_constraint);
            var r = content.Skip(1)
                .Select(s => s.Split(','))
                .Select(split => new Constraints
                {
                    TableName = split[0],
                    ColumnName = split[1],
                    Name = split[2],
                    RelatedTableName = split[3],
                    PrimaryKey = split[4]
                })
                .GroupBy(g => g.TableName)
                .ToList();

            var xElement = new XElement("Root");
            foreach (var a in r)
            {
                foreach (var database in a)
                {
                    xElement.Add(new XElement("Association", new XAttribute("Name", database.Name),
                        new XElement("End",
                            new XAttribute("Role", database.RelatedTableName),
                            new XAttribute("Type", $"Self.{database.RelatedTableName}"),
                            new XAttribute("Multiplicity", "1")),
                        new XElement("End",
                                new XAttribute("Role", database.TableName),
                                new XAttribute("Type", $"Self.{database.TableName}"),
                                new XAttribute("Multiplicity", "*")),
                            new XElement("ReferentialConstraint",
                                new XElement("Principal", new XAttribute("Role", database.RelatedTableName),
                                    new XElement("PropertyRef", new XAttribute("Name", database.ColumnName))),
                                new XElement("Dependent", new XAttribute("Role", a.Key),
                                    new XElement("PropertyRef", new XAttribute("Name", database.ColumnName))))));
                }
            }

            var xml = Path.Combine($"{_myDocument}", "csv", "5.txt");
            xElement.Save(xml);

        }

        public async Task GenerateConstraintsAsync2()
        {
            var content = await File.ReadAllLinesAsync(_constraint);
            var r = content.Skip(1)
                .Select(s => s.Split(','))
                .Select(split => new Constraints
                {
                    TableName = split[0],
                    ColumnName = split[1],
                    Name = split[2],
                    RelatedTableName = split[3],
                    PrimaryKey = split[4]
                })
                .GroupBy(g => g.TableName)
                .ToList();

            var xElement = new XElement("Root");
            foreach (var s in r)
            {
                foreach (var database in s)
                {
                    xElement.Add(new XElement("AssociationSet",
                        new XAttribute("Name", database.Name),
                        new XAttribute("Association", $"Self.{database.Name}"),
                        new XElement("End",
                            new XAttribute("Role", database.RelatedTableName),
                            new XAttribute("EntitySet", database.RelatedTableName)),
                        new XElement("End",
                            new XAttribute("Role", s.Key),
                            new XAttribute("EntitySet", s.Key))));
                }
            }

            var xml = Path.Combine($"{_myDocument}", "csv", "6.txt");
            xElement.Save(xml);
        }

        public async Task GenerateConstraintsAsync3()
        {
            var content = await File.ReadAllLinesAsync(_constraint);
            var r = content.Skip(1)
                .Select(s => s.Split(','))
                .Select(split => new Constraints
                {
                    TableName = split[0],
                    ColumnName = split[1],
                    Name = split[2],
                    RelatedTableName = split[3],
                    PrimaryKey = split[4]
                })
                .GroupBy(g => g.TableName)
                .ToList();

            var xElement = new XElement("Root");
            foreach (var s in r)
            {
                foreach (var database in s)
                {
                    xElement.Add(new XElement("NavigationProperty",
                        new XAttribute("Name", database.RelatedTableName),
                        new XAttribute("Relationship", $"Self.{database.Name}"),
                        new XAttribute("FromRole", s.Key),
                        new XAttribute("ToRole", database.RelatedTableName)
                        ));
                }
            }

            var xml = Path.Combine($"{_myDocument}", "csv", "7.txt");
            xElement.Save(xml);
        }
    }
}