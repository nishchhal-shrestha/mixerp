/********************************************************************************
Copyright (C) MixERP Inc. (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, version 2 of the License.


MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MixERP.Net.DbFactory;
using MixERP.Net.EntityParser;
using MixERP.Net.Framework;
using Npgsql;
using PetaPoco;
using Serilog;

namespace MixERP.Net.Schemas.Audit.Data
{
    /// <summary>
    /// Provides simplified data access features to perform SCRUD operation on the database table "audit.logged_actions".
    /// </summary>
    public class LoggedAction : DbAccess
    {
        /// <summary>
        /// The schema of this table. Returns literal "audit".
        /// </summary>
        public override string ObjectNamespace => "audit";

        /// <summary>
        /// The schema unqualified name of this table. Returns literal "logged_actions".
        /// </summary>
        public override string ObjectName => "logged_actions";

        /// <summary>
        /// Login id of application user accessing this table.
        /// </summary>
        public long LoginId { get; set; }

        /// <summary>
        /// User id of application user accessing this table.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The name of the database on which queries are being executed to.
        /// </summary>
        public string Catalog { get; set; }

        /// <summary>
        /// Performs SQL count on the table "audit.logged_actions".
        /// </summary>
        /// <returns>Returns the number of rows of the table "audit.logged_actions".</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public long Count()
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return 0;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to count entity \"LoggedAction\" was denied to the user with Login ID {LoginId}", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            const string sql = "SELECT COUNT(*) FROM audit.logged_actions;";
            return Factory.Scalar<long>(this.Catalog, sql);
        }

        /// <summary>
        /// Executes a select query on the table "audit.logged_actions" to return a all instances of the "LoggedAction" class to export. 
        /// </summary>
        /// <returns>Returns a non-live, non-mapped instances of "LoggedAction" class.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public IEnumerable<MixERP.Net.Entities.Audit.LoggedAction> Get()
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.ExportData, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to the export entity \"LoggedAction\" was denied to the user with Login ID {LoginId}", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            const string sql = "SELECT * FROM audit.logged_actions ORDER BY event_id;";
            return Factory.Get<MixERP.Net.Entities.Audit.LoggedAction>(this.Catalog, sql);
        }

        /// <summary>
        /// Executes a select query on the table "audit.logged_actions" with a where filter on the column "event_id" to return a single instance of the "LoggedAction" class. 
        /// </summary>
        /// <param name="eventId">The column "event_id" parameter used on where filter.</param>
        /// <returns>Returns a non-live, non-mapped instance of "LoggedAction" class mapped to the database row.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public MixERP.Net.Entities.Audit.LoggedAction Get(long eventId)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to the get entity \"LoggedAction\" filtered by \"EventId\" with value {EventId} was denied to the user with Login ID {LoginId}", eventId, this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            const string sql = "SELECT * FROM audit.logged_actions WHERE event_id=@0;";
            return Factory.Get<MixERP.Net.Entities.Audit.LoggedAction>(this.Catalog, sql, eventId).FirstOrDefault();
        }

        /// <summary>
        /// Executes a select query on the table "audit.logged_actions" with a where filter on the column "event_id" to return a multiple instances of the "LoggedAction" class. 
        /// </summary>
        /// <param name="eventIds">Array of column "event_id" parameter used on where filter.</param>
        /// <returns>Returns a non-live, non-mapped collection of "LoggedAction" class mapped to the database row.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public IEnumerable<MixERP.Net.Entities.Audit.LoggedAction> Get(long[] eventIds)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to entity \"LoggedAction\" was denied to the user with Login ID {LoginId}. eventIds: {eventIds}.", this.LoginId, eventIds);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            const string sql = "SELECT * FROM audit.logged_actions WHERE event_id IN (@0);";

            return Factory.Get<MixERP.Net.Entities.Audit.LoggedAction>(this.Catalog, sql, eventIds);
        }

        /// <summary>
        /// Custom fields are user defined form elements for audit.logged_actions.
        /// </summary>
        /// <returns>Returns an enumerable custom field collection for the table audit.logged_actions</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public IEnumerable<PetaPoco.CustomField> GetCustomFields(string resourceId)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to get custom fields for entity \"LoggedAction\" was denied to the user with Login ID {LoginId}", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            string sql;
            if (string.IsNullOrWhiteSpace(resourceId))
            {
                sql = "SELECT * FROM core.custom_field_definition_view WHERE table_name='audit.logged_actions' ORDER BY field_order;";
                return Factory.Get<PetaPoco.CustomField>(this.Catalog, sql);
            }

            sql = "SELECT * from core.get_custom_field_definition('audit.logged_actions'::text, @0::text) ORDER BY field_order;";
            return Factory.Get<PetaPoco.CustomField>(this.Catalog, sql, resourceId);
        }

        /// <summary>
        /// Displayfields provide a minimal name/value context for data binding the row collection of audit.logged_actions.
        /// </summary>
        /// <returns>Returns an enumerable name and value collection for the table audit.logged_actions</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public IEnumerable<DisplayField> GetDisplayFields()
        {
            List<DisplayField> displayFields = new List<DisplayField>();

            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return displayFields;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to get display field for entity \"LoggedAction\" was denied to the user with Login ID {LoginId}", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            const string sql = "SELECT event_id AS key, schema_name as value FROM audit.logged_actions;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                using (DataTable table = DbOperation.GetDataTable(this.Catalog, command))
                {
                    if (table?.Rows == null || table.Rows.Count == 0)
                    {
                        return displayFields;
                    }

                    foreach (DataRow row in table.Rows)
                    {
                        if (row != null)
                        {
                            DisplayField displayField = new DisplayField
                            {
                                Key = row["key"].ToString(),
                                Value = row["value"].ToString()
                            };

                            displayFields.Add(displayField);
                        }
                    }
                }
            }

            return displayFields;
        }

        /// <summary>
        /// Inserts or updates the instance of LoggedAction class on the database table "audit.logged_actions".
        /// </summary>
        /// <param name="loggedAction">The instance of "LoggedAction" class to insert or update.</param>
        /// <param name="customFields">The custom field collection.</param>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public void AddOrEdit(MixERP.Net.Entities.Audit.LoggedAction loggedAction, List<EntityParser.CustomField> customFields)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return;
            }

            object primaryKeyValue;



            if (loggedAction.EventId > 0)
            {
                primaryKeyValue = loggedAction.EventId;
                this.Update(loggedAction, loggedAction.EventId);
            }
            else
            {
                primaryKeyValue = this.Add(loggedAction);
            }

            string sql = "DELETE FROM core.custom_fields WHERE custom_field_setup_id IN(" +
                         "SELECT custom_field_setup_id " +
                         "FROM core.custom_field_setup " +
                         "WHERE form_name=core.get_custom_field_form_name('audit.logged_actions')" +
                         ");";

            Factory.NonQuery(this.Catalog, sql);

            if (customFields == null)
            {
                return;
            }

            foreach (var field in customFields)
            {
                sql = "INSERT INTO core.custom_fields(custom_field_setup_id, resource_id, value) " +
                      "SELECT core.get_custom_field_setup_id_by_table_name('audit.logged_actions', @0::character varying(100)), " +
                      "@1, @2;";

                Factory.NonQuery(this.Catalog, sql, field.FieldName, primaryKeyValue, field.Value);
            }
        }

        /// <summary>
        /// Inserts the instance of LoggedAction class on the database table "audit.logged_actions".
        /// </summary>
        /// <param name="loggedAction">The instance of "LoggedAction" class to insert.</param>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public object Add(MixERP.Net.Entities.Audit.LoggedAction loggedAction)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Create, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to add entity \"LoggedAction\" was denied to the user with Login ID {LoginId}. {LoggedAction}", this.LoginId, loggedAction);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            return Factory.Insert(this.Catalog, loggedAction);
        }

        /// <summary>
        /// Inserts or updates multiple instances of LoggedAction class on the database table "audit.logged_actions";
        /// </summary>
        /// <param name="loggedActions">List of "LoggedAction" class to import.</param>
        /// <returns></returns>
        public List<object> BulkImport(List<MixERP.Net.Entities.Audit.LoggedAction> loggedActions)
        {
            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.ImportData, this.LoginId, false);
                }

                if (!this.HasAccess)
                {
                    Log.Information("Access to import entity \"LoggedAction\" was denied to the user with Login ID {LoginId}. {loggedActions}", this.LoginId, loggedActions);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            var result = new List<object>();
            int line = 0;
            try
            {
                using (Database db = new Database(Factory.GetConnectionString(this.Catalog), Factory.ProviderName))
                {
                    using (Transaction transaction = db.GetTransaction())
                    {
                        foreach (var loggedAction in loggedActions)
                        {
                            line++;



                            if (loggedAction.EventId > 0)
                            {
                                result.Add(loggedAction.EventId);
                                db.Update(loggedAction, loggedAction.EventId);
                            }
                            else
                            {
                                result.Add(db.Insert(loggedAction));
                            }
                        }

                        transaction.Complete();
                    }

                    return result;
                }
            }
            catch (NpgsqlException ex)
            {
                string errorMessage = $"Error on line {line} ";

                if (ex.Code.StartsWith("P"))
                {
                    errorMessage += Factory.GetDBErrorResource(ex);

                    throw new MixERPException(errorMessage, ex);
                }

                errorMessage += ex.Message;
                throw new MixERPException(errorMessage, ex);
            }
            catch (System.Exception ex)
            {
                string errorMessage = $"Error on line {line} ";
                throw new MixERPException(errorMessage, ex);
            }
        }

        /// <summary>
        /// Updates the row of the table "audit.logged_actions" with an instance of "LoggedAction" class against the primary key value.
        /// </summary>
        /// <param name="loggedAction">The instance of "LoggedAction" class to update.</param>
        /// <param name="eventId">The value of the column "event_id" which will be updated.</param>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public void Update(MixERP.Net.Entities.Audit.LoggedAction loggedAction, long eventId)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Edit, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to edit entity \"LoggedAction\" with Primary Key {PrimaryKey} was denied to the user with Login ID {LoginId}. {LoggedAction}", eventId, this.LoginId, loggedAction);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            Factory.Update(this.Catalog, loggedAction, eventId);
        }

        /// <summary>
        /// Deletes the row of the table "audit.logged_actions" against the primary key value.
        /// </summary>
        /// <param name="eventId">The value of the column "event_id" which will be deleted.</param>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public void Delete(long eventId)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Delete, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to delete entity \"LoggedAction\" with Primary Key {PrimaryKey} was denied to the user with Login ID {LoginId}.", eventId, this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            const string sql = "DELETE FROM audit.logged_actions WHERE event_id=@0;";
            Factory.NonQuery(this.Catalog, sql, eventId);
        }

        /// <summary>
        /// Performs a select statement on table "audit.logged_actions" producing a paged result of 25.
        /// </summary>
        /// <returns>Returns the first page of collection of "LoggedAction" class.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public IEnumerable<MixERP.Net.Entities.Audit.LoggedAction> GetPagedResult()
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to the first page of the entity \"LoggedAction\" was denied to the user with Login ID {LoginId}.", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            const string sql = "SELECT * FROM audit.logged_actions ORDER BY event_id LIMIT 25 OFFSET 0;";
            return Factory.Get<MixERP.Net.Entities.Audit.LoggedAction>(this.Catalog, sql);
        }

        /// <summary>
        /// Performs a select statement on table "audit.logged_actions" producing a paged result of 25.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the paged result.</param>
        /// <returns>Returns collection of "LoggedAction" class.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public IEnumerable<MixERP.Net.Entities.Audit.LoggedAction> GetPagedResult(long pageNumber)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to Page #{Page} of the entity \"LoggedAction\" was denied to the user with Login ID {LoginId}.", pageNumber, this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            long offset = (pageNumber - 1) * 25;
            const string sql = "SELECT * FROM audit.logged_actions ORDER BY event_id LIMIT 25 OFFSET @0;";

            return Factory.Get<MixERP.Net.Entities.Audit.LoggedAction>(this.Catalog, sql, offset);
        }

        private List<EntityParser.Filter> GetFilters(string catalog, string filterName)
        {
            const string sql = "SELECT * FROM core.filters WHERE object_name='audit.logged_actions' AND lower(filter_name)=lower(@0);";
            return Factory.Get<EntityParser.Filter>(catalog, sql, filterName).ToList();
        }

        /// <summary>
        /// Performs a filtered count on table "audit.logged_actions".
        /// </summary>
        /// <param name="filters">The list of filter conditions.</param>
        /// <returns>Returns number of rows of "LoggedAction" class using the filter.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public long CountWhere(List<EntityParser.Filter> filters)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return 0;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to count entity \"LoggedAction\" was denied to the user with Login ID {LoginId}. Filters: {Filters}.", this.LoginId, filters);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            Sql sql = Sql.Builder.Append("SELECT COUNT(*) FROM audit.logged_actions WHERE 1 = 1");
            MixERP.Net.EntityParser.Data.Service.AddFilters(ref sql, new MixERP.Net.Entities.Audit.LoggedAction(), filters);

            return Factory.Scalar<long>(this.Catalog, sql);
        }

        /// <summary>
        /// Performs a filtered select statement on table "audit.logged_actions" producing a paged result of 25.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the paged result.</param>
        /// <param name="filters">The list of filter conditions.</param>
        /// <returns>Returns collection of "LoggedAction" class.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public IEnumerable<MixERP.Net.Entities.Audit.LoggedAction> GetWhere(long pageNumber, List<EntityParser.Filter> filters)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to Page #{Page} of the filtered entity \"LoggedAction\" was denied to the user with Login ID {LoginId}. Filters: {Filters}.", pageNumber, this.LoginId, filters);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            long offset = (pageNumber - 1) * 25;
            Sql sql = Sql.Builder.Append("SELECT * FROM audit.logged_actions WHERE 1 = 1");

            MixERP.Net.EntityParser.Data.Service.AddFilters(ref sql, new MixERP.Net.Entities.Audit.LoggedAction(), filters);

            sql.OrderBy("event_id");
            sql.Append("LIMIT @0", 25);
            sql.Append("OFFSET @0", offset);

            return Factory.Get<MixERP.Net.Entities.Audit.LoggedAction>(this.Catalog, sql);
        }

        /// <summary>
        /// Performs a filtered count on table "audit.logged_actions".
        /// </summary>
        /// <param name="filterName">The named filter.</param>
        /// <returns>Returns number of rows of "LoggedAction" class using the filter.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public long CountFiltered(string filterName)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return 0;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to count entity \"LoggedAction\" was denied to the user with Login ID {LoginId}. Filter: {Filter}.", this.LoginId, filterName);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            List<EntityParser.Filter> filters = this.GetFilters(this.Catalog, filterName);
            Sql sql = Sql.Builder.Append("SELECT COUNT(*) FROM audit.logged_actions WHERE 1 = 1");
            MixERP.Net.EntityParser.Data.Service.AddFilters(ref sql, new MixERP.Net.Entities.Audit.LoggedAction(), filters);

            return Factory.Scalar<long>(this.Catalog, sql);
        }

        /// <summary>
        /// Performs a filtered select statement on table "audit.logged_actions" producing a paged result of 25.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the paged result.</param>
        /// <param name="filterName">The named filter.</param>
        /// <returns>Returns collection of "LoggedAction" class.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public IEnumerable<MixERP.Net.Entities.Audit.LoggedAction> GetFiltered(long pageNumber, string filterName)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to Page #{Page} of the filtered entity \"LoggedAction\" was denied to the user with Login ID {LoginId}. Filter: {Filter}.", pageNumber, this.LoginId, filterName);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            List<EntityParser.Filter> filters = this.GetFilters(this.Catalog, filterName);

            long offset = (pageNumber - 1) * 25;
            Sql sql = Sql.Builder.Append("SELECT * FROM audit.logged_actions WHERE 1 = 1");

            MixERP.Net.EntityParser.Data.Service.AddFilters(ref sql, new MixERP.Net.Entities.Audit.LoggedAction(), filters);

            sql.OrderBy("event_id");
            sql.Append("LIMIT @0", 25);
            sql.Append("OFFSET @0", offset);

            return Factory.Get<MixERP.Net.Entities.Audit.LoggedAction>(this.Catalog, sql);
        }

    }
}