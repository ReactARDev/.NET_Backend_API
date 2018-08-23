namespace AMPS9000_WebAPI
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class AMPS9000DB : DbContext
    {
        public AMPS9000DB()
            : base("name=AMPS9000DB")
        {
        }

        public virtual DbSet<AlertRecipient> AlertRecipients { get; set; }
        public virtual DbSet<Alert> Alerts { get; set; }
        public virtual DbSet<AlertType> AlertTypes { get; set; }
        public virtual DbSet<AssetType> AssetTypes { get; set; }
        public virtual DbSet<BranchCommands> BranchCommands { get; set; }
        public virtual DbSet<BranchOfService> BranchOfServices { get; set; }
        public virtual DbSet<BranchSubordinateTier1> BranchSubordinateTier1 { get; set; }
        public virtual DbSet<CCIRPIR> CCIRPIRs { get; set; }
        public virtual DbSet<COCOM> COCOMs { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CommandRelationships> CommandRelationships { get; set; }
        public virtual DbSet<ComsType> ComsTypes { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<DutyPosition> DutyPositions { get; set; }
        public virtual DbSet<EEIThreat> EEIThreats { get; set; }
        public virtual DbSet<EEIOptions> EEIOptions { get; set; }
        public virtual DbSet<GroundControlSystem> GroundControlSystems { get; set; }
        public virtual DbSet<IC_ISM_Classifications> IC_ISM_Classifications { get; set; }
        public virtual DbSet<IntelReqEEI> IntelReqEEIs { get; set; }
        public virtual DbSet<IntelReqStatu> IntelReqStatus { get; set; }
        public virtual DbSet<IntelRequest> IntelRequests { get; set; }
        public virtual DbSet<LIMIDSReq> LIMIDSReqs { get; set; }
        public virtual DbSet<LocationCategory> LocationCategories { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<MapLayerCategory> MapLayerCategories { get; set; }
        public virtual DbSet<MapLayer> MapLayers { get; set; }
        public virtual DbSet<MissionType> MissionTypes { get; set; }
        public virtual DbSet<MOS_Desc> MOS_Desc { get; set; }
        public virtual DbSet<MunitionRole> MunitionRoles { get; set; }
        public virtual DbSet<Munition> Munitions { get; set; }
        public virtual DbSet<MunitionStatu> MunitionStatus { get; set; }
        public virtual DbSet<OperationArea> OperationAreas { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderType> OrderTypes { get; set; }
        public virtual DbSet<PayGrade> PayGrades { get; set; }
        public virtual DbSet<Payload> Payloads { get; set; }
        public virtual DbSet<PayloadInventory> PayloadInventory { get; set; }
        public virtual DbSet<PayloadStatu> PayloadStatus { get; set; }
        public virtual DbSet<PayloadType> PayloadTypes { get; set; }
        public virtual DbSet<PEDPersonnel> PEDPersonnels { get; set; }
        public virtual DbSet<PEDTeam> PEDTeams { get; set; }
        public virtual DbSet<PEDType> PEDTypes { get; set; }
        public virtual DbSet<Personnel> Personnels { get; set; }
        public virtual DbSet<PersonnelStatu> PersonnelStatus { get; set; }
        public virtual DbSet<PlatformCategory> PlatformCategories { get; set; }
        public virtual DbSet<PlatformRole> PlatformRoles { get; set; }
        public virtual DbSet<Platform> Platforms { get; set; }
        public virtual DbSet<PlatformInventory> PlatformInventory { get; set; }
        public virtual DbSet<PlatformStatu> PlatformStatus { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<RankClassification> RankClassifications { get; set; }
        public virtual DbSet<Rank> Ranks { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<SpecialQualification> SpecialQualifications { get; set; }
        public virtual DbSet<StatusCode> StatusCodes { get; set; }
        public virtual DbSet<StatusCodeType> StatusCodeTypes { get; set; }
        public virtual DbSet<ThreatGroup> ThreatGroups { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<MunitionsLocation> MunitionsLocations { get; set; }
        public virtual DbSet<NatlImagery> NatlImageries { get; set; }
        public virtual DbSet<MunitionsInventory> MunitionsInventory { get; set; }
        public virtual DbSet<PayloadRole> PayloadRole { get; set; }
        public virtual DbSet<CommandStructure> CommandStructure { get; set; }
        public virtual DbSet<Positions> Positions { get; set; }
        public virtual DbSet<UnitPositions> UnitPositions { get; set; }
        public virtual DbSet<Target> Targets { get; set; }
        public virtual DbSet<Objective> Objectives { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlertRecipient>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<AlertRecipient>()
                .Property(e => e.personnelID)
                .IsUnicode(false);

            modelBuilder.Entity<Alert>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<Alert>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<Alert>()
                .Property(e => e.AssetID)
                .IsUnicode(false);

            modelBuilder.Entity<Alert>()
                .Property(e => e.createUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Alert>()
                .Property(e => e.lastUpdateUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Alert>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            //modelBuilder.Entity<Alert>()
            //    .Property(e => e.LinkTo)
            //    .IsUnicode(false);

            modelBuilder.Entity<AlertType>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<AlertType>()
                .HasMany(e => e.Alerts)
                .WithRequired(e => e.AlertType)
                .HasForeignKey(e => e.Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AssetType>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<AssetType>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<AssetType>()
                .HasMany(e => e.Alerts)
                .WithOptional(e => e.AssetType1)
                .HasForeignKey(e => e.AssetType);

            modelBuilder.Entity<BranchOfService>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<BranchOfService>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<CCIRPIR>()
                .Property(e => e.CCIRPIRId)
                .IsUnicode(false);

            modelBuilder.Entity<CCIRPIR>()
                .Property(e => e.Description1)
                .IsUnicode(false);

            modelBuilder.Entity<CCIRPIR>()
                .Property(e => e.Description2)
                .IsUnicode(false);

            modelBuilder.Entity<CCIRPIR>()
                .Property(e => e.Description3)
                .IsUnicode(false);

            modelBuilder.Entity<CCIRPIR>()
                .Property(e => e.Description4)
                .IsUnicode(false);

            modelBuilder.Entity<CCIRPIR>()
                .Property(e => e.Description5)
                .IsUnicode(false);

            modelBuilder.Entity<CCIRPIR>()
                .Property(e => e.Description6)
                .IsUnicode(false);

            modelBuilder.Entity<CCIRPIR>()
                .Property(e => e.Description7)
                .IsUnicode(false);

            modelBuilder.Entity<CCIRPIR>()
                .Property(e => e.Description8)
                .IsUnicode(false);

            modelBuilder.Entity<CCIRPIR>()
                .Property(e => e.CreationDateTime);

            modelBuilder.Entity<CCIRPIR>()
                .Property(e => e.COCOMId);

            modelBuilder.Entity<CCIRPIR>()
                .Property(e => e.BranchId);

            modelBuilder.Entity<CCIRPIR>()
                .Property(e => e.CountryId)
                .IsUnicode(false);

            modelBuilder.Entity<CCIRPIR>()
                .Property(e => e.RegionId);

            modelBuilder.Entity<CCIRPIR>()
                .Property(e => e.UnitId);

            modelBuilder.Entity<CCIRPIR>()
                .Property(e => e.CommanderId)
                .IsUnicode(false);

            modelBuilder.Entity<CCIRPIR>()
                .Property(e => e.MissionName)
                .IsUnicode(false);

            modelBuilder.Entity<CCIRPIR>()
                .Property(e => e.EffectiveAreaKML)
                .IsUnicode(false);

            modelBuilder.Entity<CCIRPIR>()
                .Property(e => e.LastUpdate);

            modelBuilder.Entity<CCIRPIR>()
                .Property(e => e.LastUpdateUserId);

            modelBuilder.Entity<COCOM>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<COCOM>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<ComsType>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.displayOrder);

            modelBuilder.Entity<DutyPosition>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<DutyPosition>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<EEIThreat>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<EEIThreat>()
                .HasMany(e => e.IntelReqEEIs)
                .WithOptional(e => e.EEIThreat)
                .HasForeignKey(e => e.threatGroupID);

            modelBuilder.Entity<EEIOptions>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<GroundControlSystem>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<GroundControlSystem>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<IC_ISM_Classifications>()
                .Property(e => e.ClassificationMarkingValue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<IC_ISM_Classifications>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<IC_ISM_Classifications>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<IntelReqEEI>()
                .Property(e => e.intelReqID)
                .IsUnicode(false);

            modelBuilder.Entity<IntelReqEEI>()
                .Property(e => e.location)
                .IsUnicode(false);

            modelBuilder.Entity<IntelReqEEI>()
                .Property(e => e.gridCoordinates)
                .IsUnicode(false);

            modelBuilder.Entity<IntelReqEEI>()
                .Property(e => e.POI1_ID)
                .IsUnicode(false);

            modelBuilder.Entity<IntelReqEEI>()
                .Property(e => e.POI2_ID)
                .IsUnicode(false);

            modelBuilder.Entity<IntelReqEEI>()
                .Property(e => e.POI3_ID)
                .IsUnicode(false);

            modelBuilder.Entity<IntelReqStatu>()
                .Property(e => e.IntelRequestID)
                .IsUnicode(false);

            modelBuilder.Entity<IntelRequest>()
                .Property(e => e.IntelRequestID)
                .IsUnicode(false);

            modelBuilder.Entity<IntelRequest>()
                .Property(e => e.OrginatorPersonnelID)
                .IsUnicode(false);

            modelBuilder.Entity<IntelRequest>()
                .Property(e => e.NamedOperation)
                .IsUnicode(false);

            modelBuilder.Entity<IntelRequest>()
                .Property(e => e.PriorityIntelRequirement)
                .IsUnicode(false);

            modelBuilder.Entity<IntelRequest>()
                .Property(e => e.SpecialInstructions)
                .IsUnicode(false);

            modelBuilder.Entity<IntelRequest>()
                .Property(e => e.PrimaryPayload);

            modelBuilder.Entity<IntelRequest>()
                .Property(e => e.SecondaryPayload);

            modelBuilder.Entity<IntelRequest>()
                .Property(e => e.PointofContact)
                .IsUnicode(false);

            modelBuilder.Entity<IntelRequest>()
                .Property(e => e.ReportClassification)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<IntelRequest>()
                .Property(e => e.LIMIDSRequest)
                .IsUnicode(false);

            modelBuilder.Entity<LIMIDSReq>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<LIMIDSReq>()
                .HasMany(e => e.IntelReqEEIs)
                .WithOptional(e => e.LIMIDSReq)
                .HasForeignKey(e => e.LIMIDS_Req);

            modelBuilder.Entity<LocationCategory>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationID)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationReferenceCode)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationPhoto)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationDocument)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationMapImage)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationName)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationStreet)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationCity)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationPostalCode)
                .IsFixedLength();

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationCountry)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationLatitude);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationLongitude);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationMGRS)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationPointofContact)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationFrequency)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Location>()
                .Property(e => e.KML)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationNai)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.StateAbbrev)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.webAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.IATA)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.ICAO)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.FAA)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.UserLocationID)
                .IsUnicode(false);

            modelBuilder.Entity<MapLayerCategory>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<MapLayerCategory>()
                .Property(e => e.createUserId)
                .IsUnicode(false);

            modelBuilder.Entity<MapLayerCategory>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<MapLayer>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<MapLayer>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<MapLayer>()
                .Property(e => e.fileLocation)
                .IsUnicode(false);

            modelBuilder.Entity<MissionType>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<MissionType>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<MOS_Desc>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<MOS_Desc>()
                .Property(e => e.MOSCode)
                .IsUnicode(false);

            modelBuilder.Entity<MOS_Desc>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<MOS_Desc>()
                .Property(e => e.branchOfServiceID);

            modelBuilder.Entity<MunitionRole>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<MunitionRole>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionID)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionsReferenceCode)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionWireframe)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionPhoto)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.Munition3D)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionIcon)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.Munition2525B)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionDatasheet)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionName)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionNomenclature)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionManufacturer);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionExecutiveAgent)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionContractProgram)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionCost)
                .HasPrecision(20, 2);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionCostNotes)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionLength)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionWidthDiameter)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionWeight)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionWingspan)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionWarhead)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionEngine)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionGuideanceSys)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionLaunchPlatform)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionWeatherRestriction)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionHeight)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionWeightUnloaded)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionWeightLoaded)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionProjectileWeight)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionCaliber)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionDriveSystem)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionFeedSystem)
                .IsUnicode(false);

            modelBuilder.Entity<Munition>()
                .Property(e => e.MunitionMuzzleEnergy)
                .IsUnicode(false);

            modelBuilder.Entity<MunitionStatu>()
                .Property(e => e.MunitionInventoryID)
                .IsUnicode(false);

            modelBuilder.Entity<MunitionStatu>()
                .Property(e => e.StatusComments)
                .IsUnicode(false);

            modelBuilder.Entity<MunitionStatu>()
                .Property(e => e.lastUpdateUserId)
                .IsUnicode(false);

            modelBuilder.Entity<OperationArea>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<OperationArea>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<OperationArea>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderID)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderSubCMD1)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderSubCMD2)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderDetail)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderNotes)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderLegacyDoc)
                .IsUnicode(false);

            modelBuilder.Entity<OrderType>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<OrderType>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<OrderType>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.OrderType1)
                .HasForeignKey(e => e.OrderType);

            modelBuilder.Entity<PayGrade>()
                .Property(e => e.GradeType)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PayGrade>()
                .Property(e => e.PayGrade1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PayGrade>()
                .Property(e => e.DisplayText)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PayGrade>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadID)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadReferenceCode)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadWireframe)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadPhoto)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.Payload3D)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadIcon)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.Payload2525B)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadDatasheet)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadName)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadNomenclature)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadManufacturer);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadExecutiveAgent)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadContractProgram)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadCost)
                .HasPrecision(20, 2);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadCostNotes)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadLength)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadWidth)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadHeight)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadWeight)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadPower)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadConnector1)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadConnector2)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadCrewCount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadFrequencyRange)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadScanCoverage)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadMaximumRange)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadMapResolution)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadGroundMapping)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadStripSAR)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadSpotlightSAR)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadCCEOIR)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadGeoReferencing)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadChangeDetect)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadImageResolution)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadRefreshRateEO)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadRefreshRateIR)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadAngularCoverage)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadAreaCoverage)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadVirtualZoom)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.PayloadCrossCueing)
                .IsUnicode(false);

            modelBuilder.Entity<Payload>()
                .Property(e => e.LastUpdate);

            modelBuilder.Entity<Payload>()
                .Property(e => e.MissionRole);

            modelBuilder.Entity<PayloadInventory>()
                .Property(e => e.id);

            modelBuilder.Entity<PayloadInventory>()
                .Property(e => e.metaDataID)
                .IsUnicode(false);

            modelBuilder.Entity<PayloadInventory>()
                .Property(e => e.locationID)
                .IsUnicode(false);

            modelBuilder.Entity<PayloadInventory>()
                .Property(e => e.serialNumber)
                .IsUnicode(false);

            modelBuilder.Entity<PayloadInventory>()
                .Property(e => e.owningUnit);

            modelBuilder.Entity<PayloadInventory>()
                .Property(e => e.lastUpdate);

            modelBuilder.Entity<PayloadInventory>()
                .Property(e => e.lastUpdateUserId)
                .IsUnicode(false);

            modelBuilder.Entity<PayloadStatu>()
                .Property(e => e.PayloadInventoryID)
                .IsUnicode(false);

            modelBuilder.Entity<PayloadStatu>()
                .Property(e => e.StatusComments)
                .IsUnicode(false);

            modelBuilder.Entity<PayloadStatu>()
                .Property(e => e.lastUpdateUserId)
                .IsUnicode(false);

            modelBuilder.Entity<PayloadType>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<PayloadType>()
                .Property(e => e.abbreviation)
                .IsUnicode(false);

            modelBuilder.Entity<PEDPersonnel>()
                .Property(e => e.PEDID)
                .IsUnicode(false);

            modelBuilder.Entity<PEDPersonnel>()
                .Property(e => e.PersonnelID)
                .IsUnicode(false);

            modelBuilder.Entity<PEDTeam>()
                .Property(e => e.PEDID)
                .IsUnicode(false);

            modelBuilder.Entity<PEDTeam>()
                .Property(e => e.Identifier)
                .IsUnicode(false);

            modelBuilder.Entity<PEDTeam>()
                .Property(e => e.StatusComments)
                .IsUnicode(false);

            modelBuilder.Entity<PEDTeam>()
                .Property(e => e.lastUpdateUserId)
                .IsUnicode(false);

            modelBuilder.Entity<PEDTeam>()
                .HasMany(e => e.PEDPersonnels)
                .WithRequired(e => e.PEDTeam)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PEDType>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<PEDType>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<PEDType>()
                .Property(e => e.code)
                .IsUnicode(false);

            modelBuilder.Entity<PEDType>()
                .HasMany(e => e.PEDTeams)
                .WithRequired(e => e.PEDType)
                .HasForeignKey(e => e.TypeCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.PersonnelID)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.PersonnelReferenceCode)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.PersonnelPhoto)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.OrganizationLogo)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.Datasheet)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.MiddleInitial)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.Nationality)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.Clearance)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.CACid)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.CallSign)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.DSN)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.EmailNIPR)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.EmailSIPR)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.ChatID)
                .IsUnicode(false);

            modelBuilder.Entity<PersonnelStatu>()
                .Property(e => e.PersonnelID)
                .IsUnicode(false);

            modelBuilder.Entity<PersonnelStatu>()
                .Property(e => e.PersonnelRemarks)
                .IsUnicode(false);

            modelBuilder.Entity<PersonnelStatu>()
                .Property(e => e.lastUpdateUserId)
                .IsUnicode(false);

            modelBuilder.Entity<PlatformCategory>()
                .Property(e => e.description)
                .IsFixedLength();

            modelBuilder.Entity<PlatformCategory>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<PlatformCategory>()
                .Property(e => e.abbreviation)
                .IsUnicode(false);

            modelBuilder.Entity<PlatformRole>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<PlatformRole>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformID)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformWireframe)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformPhoto)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.Platform3D)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformIcon)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.Platform2525B)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformDatasheet)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformName)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformNomenclature)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformManufacturer);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformExecutiveAgent)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformContractProgram)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformCost)
                .HasPrecision(20, 2);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformLength)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformWingspan)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformHeight)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformWeight)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformPowerPlant)
                .IsUnicode(false);

            modelBuilder.Entity<Platform>()
                .Property(e => e.PlatformPayloadCapacity)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Platform>()
                .Property(e => e.LastUpdate);

            modelBuilder.Entity<PlatformInventory>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<PlatformInventory>()
                .Property(e => e.metaDataID)
                .IsUnicode(false);

            modelBuilder.Entity<PlatformInventory>()
                .Property(e => e.locationID)
                .IsUnicode(false);

            modelBuilder.Entity<PlatformInventory>()
                .Property(e => e.owningUnit);

            modelBuilder.Entity<PlatformInventory>()
                .Property(e => e.tailNumber)
                .IsUnicode(false);

            modelBuilder.Entity<PlatformInventory>()
                .Property(e => e.payload1)
                .IsUnicode(false);

            modelBuilder.Entity<PlatformInventory>()
                .Property(e => e.payload2)
                .IsUnicode(false);

            modelBuilder.Entity<PlatformInventory>()
                .Property(e => e.payload3)
                .IsUnicode(false);

            modelBuilder.Entity<PlatformInventory>()
                .Property(e => e.armament1)
                .IsUnicode(false);

            modelBuilder.Entity<PlatformInventory>()
                .Property(e => e.armament2)
                .IsUnicode(false);

            modelBuilder.Entity<PlatformInventory>()
                .Property(e => e.armament3)
                .IsUnicode(false);

            modelBuilder.Entity<PlatformInventory>()
                .Property(e => e.coms1);

            modelBuilder.Entity<PlatformInventory>()
                .Property(e => e.coms2);

            modelBuilder.Entity<PlatformInventory>()
                .Property(e => e.lastUpdate);

            modelBuilder.Entity<PlatformInventory>()
                .Property(e => e.lastUpdateUserId)
                .IsUnicode(false);

            modelBuilder.Entity<PlatformStatu>()
                .Property(e => e.PlatformInventoryID)
                .IsUnicode(false);

            modelBuilder.Entity<PlatformStatu>()
                .Property(e => e.StatusComments)
                .IsUnicode(false);

            modelBuilder.Entity<PlatformStatu>()
                .Property(e => e.lastUpdateUserId)
                .IsUnicode(false);

            modelBuilder.Entity<RankClassification>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<RankClassification>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<Rank>()
                .Property(e => e.description)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rank>()
                .Property(e => e.rankAbbreviation)
                .IsUnicode(false);

            modelBuilder.Entity<Rank>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<Region>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Region>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<SpecialQualification>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<SpecialQualification>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<StatusCode>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<StatusCode>()
                .Property(e => e.displayOrder)
                .IsFixedLength();

            modelBuilder.Entity<StatusCode>()
                .Property(e => e.languageCode)
                .IsUnicode(false);

            modelBuilder.Entity<ThreatGroup>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<ThreatGroup>()
                .Property(e => e.groupCode)
                .IsUnicode(false);

            modelBuilder.Entity<Unit>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Unit>()
                .Property(e => e.UnitIdentificationCode)
                .IsUnicode(false);

            modelBuilder.Entity<Unit>()
                .Property(e => e.DerivativeUIC)
                .IsUnicode(false);

            modelBuilder.Entity<Unit>()
                .Property(e => e.JointManningDocument)
                .IsUnicode(false);

            modelBuilder.Entity<Unit>()
                .Property(e => e.LocationID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.PersonnelID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UnitIdentificationCode)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.PasswordSalt)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.PEDTeams)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.lastUpdateUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MunitionsLocation>()
                .Property(e => e.locationID)
                .IsUnicode(false);

            modelBuilder.Entity<MunitionsLocation>()
                .Property(e => e.munitionID)
                .IsUnicode(false);

            modelBuilder.Entity<NatlImagery>()
                .Property(e => e.NatImageryID)
                .IsUnicode(false);

            modelBuilder.Entity<NatlImagery>()
                .Property(e => e.ReferenceCode)
                .IsUnicode(false);

            modelBuilder.Entity<NatlImagery>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<NatlImagery>()
                .Property(e => e.MGRS)
                .IsUnicode(false);

            modelBuilder.Entity<NatlImagery>()
                .Property(e => e.NatlImgaryImage)
                .IsUnicode(false);

            modelBuilder.Entity<BranchCommands>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<BranchCommands>()
                .Property(e => e.abbreviation)
                .IsUnicode(false);

            modelBuilder.Entity<BranchCommands>()
                .Property(e => e.HQCity)
                .IsUnicode(false);

            modelBuilder.Entity<BranchCommands>()
                .Property(e => e.HQCountryCode)
                .IsUnicode(false);

            modelBuilder.Entity<BranchCommands>()
                .Property(e => e.HQStateAbbrev)
                .IsUnicode(false);

            modelBuilder.Entity<BranchCommands>()
                .HasMany(e => e.BranchSubordinateTier1)
                .WithRequired(e => e.BranchCommands)
                .HasForeignKey(e => e.branchCommandID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BranchSubordinateTier1>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<BranchSubordinateTier1>()
                .Property(e => e.HQCity)
                .IsUnicode(false);

            modelBuilder.Entity<BranchSubordinateTier1>()
                .Property(e => e.HQStateAbbrev)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BranchSubordinateTier1>()
                .Property(e => e.HQCountryCode)
                .IsUnicode(false);

            modelBuilder.Entity<BranchSubordinateTier1>()
                .Property(e => e.webAddress)
                .IsUnicode(false);

            modelBuilder.Entity<CommandRelationships>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<MunitionsInventory>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<MunitionsInventory>()
                .Property(e => e.metaDataID)
                .IsUnicode(false);

            modelBuilder.Entity<MunitionsInventory>()
                .Property(e => e.locationID)
                .IsUnicode(false);

            modelBuilder.Entity<MunitionsInventory>()
                .Property(e => e.serialNumber)
                .IsUnicode(false);

            modelBuilder.Entity<MunitionsInventory>()
                .Property(e => e.lastUpdateUserId)
                .IsUnicode(false);

            modelBuilder.Entity<PayloadRole>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<CommandStructure>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<CommandStructure>()
                .Property(e => e.superiorCommandStructID)
                .IsUnicode(false);

            modelBuilder.Entity<CommandStructure>()
                .Property(e => e.lastUpdateUserID)
                .IsUnicode(false);

            modelBuilder.Entity<UnitPositions>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<UnitPositions>()
                .Property(e => e.personnelID)
                .IsUnicode(false);

            modelBuilder.Entity<UnitPositions>()
                .Property(e => e.vacantInd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<UnitPositions>()
                .Property(e => e.Paragraph)
                .IsUnicode(false);

            modelBuilder.Entity<UnitPositions>()
                .Property(e => e.Line)
                .IsUnicode(false);
        }
    }
}
