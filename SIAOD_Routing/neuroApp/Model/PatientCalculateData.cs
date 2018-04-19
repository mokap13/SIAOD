using neuroApp.Analyzes.AccompanyingIllness;
using neuroApp.Analyzes.ClinicalLaboratoryData;
using neuroApp.Analyzes.Complaint;
using neuroApp.Analyzes.HIV;
using neuroApp.Analyzes.HIVAssociateDisease;
using neuroApp.Analyzes.ObjectiveStatus;
using neuroApp.Analyzes.Parameters;
using neuroApp.Analyzes.Tuberculosis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp.Model
{
    public class PatientCalculateData
    {
        //IDictionary<string, MedicalParameter> parameters;
        public PatientCalculateData()
        {
            //this.parameters = parameters;
            //parameters = new Dictionary<string, MedicalParameter>
            //{
            //    {"Приём АРВТ", new InversedTresholdMedicalParameter(0.5,1.907M)},
            //    {"Дефицит массы тела", new MedicalParameter(0.5,1.907M)},
            //    {"Наличие рецидива туберкулёзного процесса", new MedicalParameter(0.5,1.907M)},
            //    {"Наличие деструкции легочной ткани", new MedicalParameter(0.5,1.907M)},
            //    {"Длительность ВИЧ-инфекцииболезни", new MedicalParameter(0.5,1.907M)},
            //    {"Наркомания", new MedicalParameter(0.5,1.907M)},
            //    {"Тахикардия", new MedicalParameter(0.5,1.907M)},
            //    {"Аритмия", new MedicalParameter(0.5,1.907M)},
            //    {"Повышение емпературы тела", new MedicalParameter(0.5,1.907M)},
            //    {"Гепатомегалия", new MedicalParameter(0.5,1.907M)},
            //    {"Спленомегалия", new MedicalParameter(0.5,1.907M)},
            //    {"Лимфаденопатия", new MedicalParameter(0.5,1.907M)},
            //    {"Вирусная нагрузка ВИЧ", new MedicalParameter(0.5,1.907M)},
            //    {"СД4", new MedicalParameter(0.5,1.907M)},
            //    {"Гемоглобин", new MedicalParameter(0.5,1.907M)},
            //    {"СОЭ", new MedicalParameter(0.5,1.907M)},
            //    {"Лейкоциты", new MedicalParameter(0.5,1.907M)},
            //    {"Лимфоциты", new MedicalParameter(0.5,1.907M)},
            //    {"Тромбоциты", new MedicalParameter(0.5,1.907M)},
            //    {"АЛТ", new MedicalParameter(0.5,1.907M)},
            //    {"АСТ", new MedicalParameter(0.5,1.907M)},
            //    {"Общий биллирубин", new MedicalParameter(0.5,1.907M)},
            //    {"Креатинин", new MedicalParameter(0.5,1.907M)},
            //    {"Бактериовыделение", new MedicalParameter(0.5,1.907M)},
            //    {"Наличие парентеральных гепатитов", new MedicalParameter(0.5,1.907M)},
            //    {"Сахарный диабет", new MedicalParameter(0.5,1.907M)},
            //    {"Наличие ВИЧ-ассоциированных заболеваний", new MedicalParameter(0.5,1.907M)}
            //};
        }
        public virtual List<AccompanyingIllness> AccompanyingIllnesses { get; set; }
        public virtual BloodChemistry BloodChemistry { get; set; }
        public virtual CompleteBloodCount CompleteBloodCount { get; set; }
        public virtual Immunogram Immunogram { get; set; }
        public virtual HIV HIV { get; set; }
        public virtual TuberculosisForm TuberculosisForm { get; set; }
        public virtual List<HIVStatus> HIVStatuses { get; set; }
        public virtual List<HIVAssociateDisease> HIVAssociateDiseases { get; set; }
        public virtual ObjectiveStatus ObjectiveStatus { get; set; }
        public virtual List<ObjectiveStatusDisease> ObjectiveStatusDiseases { get; set; }
        public virtual List<DrugResistance> DrugResistances { get; set; }
        public virtual List<TuberculosisStatus> TuberculosisStatuses { get; set; }

        //27
        #region Изменяющиеся уставки
        public double ViralLoadTreshold
        {
            get
            {
                List<double> newTresholds = new List<double>()
                {
                    Immunogram.ViralLoadTreshold,
                    TuberculosisFormIdGenralization() > 2 ? 6796:double.MaxValue,
                    HasLenthDestructionTreshold > 0 ? 145416:double.MaxValue,
                    IsDruggerTreshold > 0 ? 128050:double.MaxValue,
                    HasTahicardyaTreshold > 0 ? 560236:double.MaxValue,
                    !IsArytmiaNormal ? 6796:double.MaxValue,
                    !IsPirexiaNormal ? 175026.5:double.MaxValue,
                    !IsHepatomegaliaNormal ? 128050:double.MaxValue,
                    !IsSplenomegaliaNormal ? 226342:double.MaxValue,
                    CompleteBloodCount.Lymphocytes < 18.5 ? 7000.5:double.MaxValue,
                    BloodChemistry.ALT > 0.285 ? 341751.5:double.MaxValue,
                    !IsBacteriovidelenieNormal ? 221500:double.MaxValue,
                };
                return newTresholds.Min();
            }
        }
        public double Cd4Treshold
        {
            get
            {
                List<double> newTresholds = new List<double>()
                {
                    Immunogram.Cd4Treshold,
                    !IsHepatomegaliaNormal ? 177:double.MaxValue,
                    !IsSplenomegaliaNormal ? 177:double.MaxValue,
                    CompleteBloodCount.Platelets < 212.5? 177:double.MaxValue,
                    BloodChemistry.ALT > 1.065 ? 97.5:double.MaxValue,
                    BloodChemistry.TotalBilirubin > 21.8 ? 177:double.MaxValue,
                };
                return newTresholds.Min();
            }
        }
        public double HemoglobinTreshold
        {
            get
            {
                List<double> newTresholds = new List<double>()
                {
                    CompleteBloodCount.HemoglobinTreshold,
                    CompleteBloodCount.Platelets < 156.5 ? 118.5:double.MaxValue,
                };
                return newTresholds.Min();
            }
        }
        public double EsrTreshold
        {
            get
            {
                List<double> newTresholds = new List<double>()
                {
                    CompleteBloodCount.EsrTreshold,
                    CompleteBloodCount.Hemoglobin < 117 ? 33.5:double.MaxValue,
                    BloodChemistry.AST > 0.245 ? 35.5:double.MaxValue,
                };
                return newTresholds.Min();
            }
        }
        public double LymphozytesTreshold
        {
            get
            {
                List<double> newTresholds = new List<double>()
                {
                    CompleteBloodCount.LymphocytesTreshold,
                    Immunogram.ViralLoad > 7000.5 ? 18.5:double.MaxValue,
                };
                return newTresholds.Min();
            }
        }
        public double PlateletsTreshold
        {
            get
            {
                List<double> newTresholds = new List<double>()
                {
                    CompleteBloodCount.PlateletsTreshold,
                    Immunogram.CD4 < 177 ? 212.5:double.MaxValue,
                    CompleteBloodCount.Hemoglobin < 118.5 ? 156.5:double.MaxValue,
                    !IsHivAssociateDiseassesNormal ? 182.5:double.MaxValue,
                };
                return newTresholds.Min();
            }
        }
        public double AltTreshold
        {
            get
            {
                List<double> newTresholds = new List<double>()
                {
                    BloodChemistry.AltTreshold,
                    !IsRecediveTuberculosisNormal? 0.295:double.MaxValue,
                    HIV.Duration > 11.5 ? 0.455:double.MaxValue,
                    Immunogram.CD4 < 97.5 ? 1.065:double.MaxValue,
                    !IsBacteriovidelenieNormal ? 0.295:double.MaxValue,
                };
                return newTresholds.Min();
            }
        }
        public double AstTreshold
        {
            get
            {
                List<double> newTresholds = new List<double>()
                {
                    BloodChemistry.AltTreshold,
                    !IsRecediveTuberculosisNormal? 0.265:double.MaxValue,
                    !IsDestructionTuberculosisNormal? 0.265:double.MaxValue,
                    HIV.Duration > 11.5 ? 0.6:double.MaxValue,
                    CompleteBloodCount.ESR > 35.5 ? 0.245:double.MaxValue,
                    BloodChemistry.TotalBilirubin > 21.8 ? 1.285:double.MaxValue,
                };
                return newTresholds.Min();
            }
        }
        public double TotalBilirubinTreshold
        {
            get
            {
                List<double> newTresholds = new List<double>()
                {
                    BloodChemistry.TotalBilirubinTreshold,
                    !IsRecediveTuberculosisNormal? 21.8:double.MaxValue,
                    !IsDruggerNormal? 21.185:double.MaxValue,
                    ObjectiveStatus.Tachycardia? 21.8:double.MaxValue,
                    ObjectiveStatus.Pirexia? 21.8:double.MaxValue,
                    !IsHepatomegaliaNormal? 21.8:double.MaxValue,
                    !IsSplenomegaliaNormal? 19.65:double.MaxValue,
                    Immunogram.CD4 < 177 ? 21.8:double.MaxValue,
                    BloodChemistry.AST > 1.285?  21.8:double.MaxValue,
                    !IsHivAssociateDiseassesNormal? 19.65: double.MaxValue,
                };
                return newTresholds.Min();
            }
        }
        public double CreatinineTreshold
        {
            get
            {
                List<double> newTresholds = new List<double>()
                {
                    BloodChemistry.CreatinineTreshold,
                    !IsParanteralHepatitsNormal ? 147.05:double.MaxValue,
                };
                return newTresholds.Min();
            }
        }
        public double HivDurationTreshold
        {
            get
            {
                List<double> newTresholds = new List<double>()
                {
                    HIV.DurationTreshold,
                    BloodChemistry.ALT > 0.455 ? 11.5:double.MaxValue,
                    BloodChemistry.AST > 0.6 ? 11.5:double.MaxValue,
                };
                return newTresholds.Min();
            }
        }
        public double HasParenteralHipotitsTreshold
        {
            get
            {
                List<double> newTresholds = new List<double>()
                {
                    ParenteralHipotitsTresholdStandart,
                    BloodChemistry.Creatinine > 147.05 ? 1:double.MaxValue,
                };
                return newTresholds.Min();
            }
        }
        public double HasHivAssociateDiseasesTreshold
        {
            get
            {
                List<double> newTresholds = new List<double>()
                {
                    HivAssociatedDiseaseTresholdStandart,
                    CompleteBloodCount.Platelets < 182.5? 1:double.MaxValue,
                    BloodChemistry.TotalBilirubin > 19.65 ? 1:double.MaxValue
                };
                return newTresholds.Min();
            }
        }
        #endregion

        #region Статические уставки 
        private double ArvtTreshold = 0.5;
        private double BodyWeigthTreshold = 0.5;
        private double HasRecediveTuberculosisTreshold = 0.5;
        private double HasLenthDestructionTreshold = 0.5;
        private double IsDruggerTreshold = 0.5;
        private double HasTahicardyaTreshold = 0.5;
        private double HasArytmyaTreshold = 0.5;
        private double HasPirexiaTreshold = 0.5;
        private double HasHepotomegaliaTreshold = 0.5;
        private double HasSplenomegaliaTreshold = 0.5;
        private double HasLimphodenatopiaTreshold = 0.5;
        private double LeycozytesTreshold = 3.65;
        private double BacterialTreshold = 0.5;
        private double HasDiabetTreshold = 0.5;
        private double ParenteralHipotitsTresholdStandart = 1.5;
        private double HivAssociatedDiseaseTresholdStandart = 2.5;
        #endregion
        


        #region Common
        private int TuberculosisFormIdGenralization()
        {
            if (((TuberculosisFormEnum)TuberculosisForm.Id == TuberculosisFormEnum.ContactTuberculosis
                    || (TuberculosisFormEnum)TuberculosisForm.Id == TuberculosisFormEnum.ClinicalHeal))
            {
                return 0;
            }
            else if (((TuberculosisFormEnum)TuberculosisForm.Id == TuberculosisFormEnum.Ochag))
            {
                return 1;
            }
            else if ((TuberculosisFormEnum)TuberculosisForm.Id == TuberculosisFormEnum.Infiltrative)
            {
                return 2;
            }
            else if ((TuberculosisFormEnum)TuberculosisForm.Id == TuberculosisFormEnum.Dissimilar)
            {
                return 3;
            }
            else if ((TuberculosisFormEnum)TuberculosisForm.Id == TuberculosisFormEnum.FibroCavernate)
            {
                return 4;
            }
            else
            {
                return 5;
            }
        }
        private bool IsArvtNormal
        {
            get
            {
                if (HIVStatuses.Exists(f => (HIVStatusesEnum)f.Id == HIVStatusesEnum.ARVT))
                {
                    return false;
                }
                return true;
            }
        }
        private bool IsDruggerNormal
        {
            get
            {
                if ((HIVStatuses != null)
                    && (HIVStatuses.Exists(f => (HIVStatusesEnum)f.Id == HIVStatusesEnum.Drugger)))
                {
                    return false;
                }
                return true;
            }
        }
        private bool IsBodyWeigthNormal
        {
            get
            {
                return !ObjectiveStatus.BodyWeightDefecit;
            }
        }
        private bool IsRecediveTuberculosisNormal
        {
            get
            {
                if (TuberculosisStatuses.Exists(e => (TuberculosisStatusesEnum)e.Id == TuberculosisStatusesEnum.Recedive))
                    return false;
                return true;
            }
        }
        private bool IsDestructionTuberculosisNormal
        {
            get
            {
                if (TuberculosisStatuses.Exists(e => (TuberculosisStatusesEnum)e.Id == TuberculosisStatusesEnum.Destruction))
                    return false;
                return true;
            }
        }
        private bool IsTachycardiaNormal
        {
            get
            {
                return !ObjectiveStatus.Tachycardia;
            }
        }
        private bool IsArytmiaNormal
        {
            get
            {
                if (ObjectiveStatusDiseases.Exists(e => (ObjectiveStatusDiseasesEnum)e.Id == ObjectiveStatusDiseasesEnum.Arrhytmia))
                    return false;
                return true;
            }
        }
        private bool IsLimphodenatopiaNormal
        {
            get
            {
                if (ObjectiveStatusDiseases.Exists(e => (ObjectiveStatusDiseasesEnum)e.Id == ObjectiveStatusDiseasesEnum.Lymphodenatopia))
                    return false;
                return true;
            }
        }
        private bool IsSplenomegaliaNormal
        {
            get
            {
                if (ObjectiveStatusDiseases.Exists(e => (ObjectiveStatusDiseasesEnum)e.Id == ObjectiveStatusDiseasesEnum.Splenomegalia))
                    return false;
                return true;
            }
        }
        private bool IsHepatomegaliaNormal
        {
            get
            {
                if (ObjectiveStatusDiseases.Exists(e => (ObjectiveStatusDiseasesEnum)e.Id == ObjectiveStatusDiseasesEnum.Hepatomegalia))
                    return false;
                return true;
            }
        }
        private bool IsBacteriovidelenieNormal
        {
            get
            {
                if (TuberculosisStatuses.Exists(e => (TuberculosisStatusesEnum)e.Id == TuberculosisStatusesEnum.Bacteriovidelenie))
                    return false;
                return true;
            }
        }
        private bool IsPirexiaNormal
        {
            get
            {
                return !ObjectiveStatus.Pirexia;
            }
        }
        private bool IsDiabetNormal
        {
            get
            {
                if (AccompanyingIllnesses.Exists(e => (AccompanyingIllnessesEnum)e.Id == AccompanyingIllnessesEnum.Diabet))
                    return false;
                return true;
            }
        }
        private bool IsHivAssociateDiseassesNormal
        {
            get
            {
                if (HIVAssociateDiseases.Count > HivAssociatedDiseaseTresholdStandart)
                    return false;
                return true;
            }
        }
        private bool IsParanteralHepatitsNormal
        {
            get
            {
                if (AccompanyingIllnesses.Count > ParenteralHipotitsTresholdStandart)
                    return false;
                return true;
            }
        }

        #endregion

        private double ArvtWeight = 1.907;
        private double TuberculosisFormWeight
        {
            get
            {
                if (TuberculosisFormIdGenralization() == 0)
                {
                    return 0.465;
                }
                else if (TuberculosisFormIdGenralization() == 1)
                {
                    return 1.047;
                }
                else if (TuberculosisFormIdGenralization() == 2)
                {
                    return 1.647;
                }
                else if (TuberculosisFormIdGenralization() == 3)
                {
                    return 8.888;
                }
                else if (TuberculosisFormIdGenralization() == 4)
                {
                    return 3.458;
                }
                else
                {
                    return 7.743;
                }
            }
        }
        private double BodyWeigthWeith = 2.161;
        private double RecediveTuberculosisWeigth = 7.271;
        private double LenthDestructionWeigth = 1.61;
        private double HivDurationWeigth = 3.931;
        private double DruggerWeight
        {
            get
            {
                if ((HIVStatuses != null)
                    && (HIVStatuses.Exists(f => (HIVStatusesEnum)f.Id == HIVStatusesEnum.Drugger)))
                {
                    return 0.933;
                }
                return 0;
            }
        }
        private double TahicardyaWeigth = 0.359;
        private double ArytmyaWeigth = 3.373;
        private double PirexiaWeigth = 1.515;
        private double HepotomegaliaWeigth = 0.83;
        private double SplenomegaliaWeigth = 2.161;
        private double LimphodenatopiaWeigth = 1.029;
        private double ViralLoadWeigth = 7.2;
        private double Cd4Weigth = 14.4;
        private double HemoglobinWeigth = 0.234;
        private double EsrWeigth = 1.123;
        private double LeycozytesWeigth = 0.202;
        private double LymphozytesWeigth = 1.372;
        private double PlateletsWeigth = 0.434;
        private double AltWeigth = 1.326;
        private double AstWeigth = 1.326;
        private double TotalBilirubinWeigth = 0.253;
        private double CreatinineWeigth = 0.694;
        private double BacterialWeigth = 2.774;
        private double HasParenteralHipotitsWeigth = 5.221;
        private double DiabetWeigth = 1.74;
        private double HasHivAssociateDiseasesWeigth = 11.373;

        public double CalculateRisk()
        {
            double risk = 0;
            if (!IsArvtNormal)
                risk += ArvtWeight;
            risk += TuberculosisFormWeight;
            if (!IsBodyWeigthNormal)
                risk += BodyWeigthWeith;
            if (!IsRecediveTuberculosisNormal)
                risk += RecediveTuberculosisWeigth;
            if (!IsDestructionTuberculosisNormal)
                risk += LenthDestructionWeigth;
            if (HIV.Duration > HivDurationTreshold)//
                risk += HivDurationWeigth;
            if (!IsDruggerNormal)
                risk += DruggerWeight;
            if (!IsTachycardiaNormal)
                risk += TahicardyaWeigth;
            if (!IsArytmiaNormal)
                risk += ArytmyaWeigth;
            if (!IsPirexiaNormal)
                risk += PirexiaWeigth;
            if (!IsHepatomegaliaNormal)
                risk += HepotomegaliaWeigth;
            if (!IsSplenomegaliaNormal)
                risk += SplenomegaliaWeigth;
            if (!IsLimphodenatopiaNormal)
                risk += LimphodenatopiaWeigth;
            if (Immunogram.ViralLoad > ViralLoadTreshold)
                risk += ViralLoadWeigth;
            if (Immunogram.CD4 < Cd4Treshold)
                risk += Cd4Weigth;
            if (CompleteBloodCount.Hemoglobin < HemoglobinTreshold)
                risk += HemoglobinWeigth;
            if (CompleteBloodCount.ESR > EsrTreshold)
                risk += EsrWeigth;
            if (CompleteBloodCount.Leukocytes < LeycozytesTreshold)
                risk += LeycozytesWeigth;
            if (CompleteBloodCount.Lymphocytes < LymphozytesTreshold)
                risk += LymphozytesWeigth;
            if (CompleteBloodCount.Platelets < PlateletsTreshold)
                risk += PlateletsWeigth;
            if (BloodChemistry.ALT > AltTreshold)
                risk += AltWeigth;
            if (BloodChemistry.AST > AstTreshold)
                risk += AstWeigth;
            if (BloodChemistry.TotalBilirubin > TotalBilirubinTreshold)
                risk += TotalBilirubinWeigth;
            if (BloodChemistry.Creatinine > CreatinineTreshold)
                risk += CreatinineWeigth;
            if (!IsBacteriovidelenieNormal)
                risk += BacterialWeigth;
            if (AccompanyingIllnesses.Count > HasParenteralHipotitsTreshold)
                risk += HasParenteralHipotitsWeigth;
            if (!IsDiabetNormal)
                risk += DiabetWeigth;
            if (HIVAssociateDiseases.Count > HasHivAssociateDiseasesTreshold)
                risk += HasHivAssociateDiseasesWeigth;

            return risk;
        }
    }

    enum HIVStatusesEnum
    {
        Drugger = 1,
        ARVT = 2
    }
    enum TuberculosisFormEnum
    {
        ContactTuberculosis = 1, //0
        ClinicalHeal = 2,//0
        Ochag = 3,//1
        Infiltrative = 4,//2
        Dissimilar = 5,//3
        TVGLU = 6,//1
        Milliar = 7,//5
        FibroCavernate = 8,//4
        PlevritTuberculosis = 9,//3
        LengthOutTubercolosis = 10,//3
        CaseosPnevmo = 12,//5
        Cavernozni = 12,//4
        Cirotichesky = 13,//4
        TuberculosisCNS = 14//5
    }
    enum TuberculosisStatusesEnum
    {
        Recedive = 1,
        Destruction = 2,
        PTP = 3,
        FalseRentgenDynamic = 4,
        Bacteriovidelenie = 5
    }
    enum ObjectiveStatusDiseasesEnum
    {
        Lymphodenatopia = 1,
        Arrhytmia = 2,
        Splenomegalia = 3,
        Hepatomegalia = 4,
    }
    enum AccompanyingIllnessesEnum
    {
        Diabet = 5,
        //1	Хронический вирусный гепатит С
        //2	Хронический вирусный гепатит B
        //3	Токсический гепатит
        //4	Болезни почек
        //5	Сахарный диабет
        //6	Болезни ЖКТ
        //7	Остеопороз
        //8	Не-СПИД онкология
        //9	СПИД-индикаторные заболевания
        //10 Болезни сердечно-сосудистой системы
    }
}
