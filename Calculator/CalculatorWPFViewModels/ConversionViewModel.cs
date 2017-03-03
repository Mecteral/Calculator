﻿using System.Collections.Generic;
using Calculator.Logic.ArgumentParsing;
using Caliburn.Micro;

namespace CalculatorWPFViewModels
{
    public class ConversionViewModel : PropertyChangedBase
    {
        public List<string> MetricalMasses { get; set; }= new List<string>();
        public List<string> MetricalVolumes { get; set; }= new List<string>();
        public List<string> MetricalAreas { get; set; }= new List<string>();
        public List<string> MetricalLengths { get; set; }= new List<string>();
        public List<string> ImperialMasses { get; set; }= new List<string>();
        public List<string> ImperialVolumes { get; set; }= new List<string>();
        public List<string> ImperialAreas { get; set; }= new List<string>();
        public List<string> ImperialLengths { get; set; }= new List<string>();
        readonly IApplicationArguments mArguments;
        string mAcre;
        string mCentiLiter;
        string mCentimeter;
        string mChain;
        string mDrachm;
        string mFathom;
        string mFluidOunce;
        string mFoot;
        string mFurlong;
        string mGallon;
        string mGill;
        string mGrain;
        string mGram;
        string mHectar;
        string mHectoliter;
        string mHundredWeight;
        string mImperialTon;
        string mInch;
        string mKilogram;
        string mKilometer;
        string mLeague;
        string mLiter;
        string mMeter;
        string mMile;
        string mMilligram;
        string mMilliliter;
        string mMillimeter;
        string mOunce;
        string mPerch;
        string mPint;
        string mPound;
        string mQuart;
        string mRood;
        string mSquarecentimeter;
        string mSquarefoot;
        string mSquarekilometer;
        string mSquaremeter;
        string mSquaremillimeter;
        string mStone;
        string mThough;
        bool mToImperial;
        bool mToMetric;
        string mTon;
        string mYard;

        public ConversionViewModel(IApplicationArguments arguments)
        {
            mArguments = arguments;
        }

        public bool ToMetric
        {
            get { return mToMetric; }
            set
            {
                if (value == mToMetric) return;
                mToMetric = value;
                NotifyOfPropertyChange(() => ToMetric);
                mArguments.ToMetric = true;
            }
        }

        public bool ToImperial
        {
            get { return mToImperial; }
            set
            {
                if (value == mToImperial) return;
                mToImperial = value;
                NotifyOfPropertyChange(() => ToImperial);
                mArguments.ToMetric = false;
            }
        }

        public string Milliliter
        {
            get { return mMilliliter; }
            set
            {
                if (value == mMilliliter) return;
                mMilliliter = value;
                NotifyOfPropertyChange(() => Milliliter);
                SetUnitAbbreviation("ml");
            }
        }

        public string CentiLiter
        {
            get { return mCentiLiter; }
            set
            {
                if (value == mCentiLiter) return;
                mCentiLiter = value;
                NotifyOfPropertyChange(() => CentiLiter);
                SetUnitAbbreviation("cl");
            }
        }

        public string Liter
        {
            get { return mLiter; }
            set
            {
                if (value == mLiter) return;
                mLiter = value;
                NotifyOfPropertyChange(() => Liter);
                SetUnitAbbreviation("l");
            }
        }

        public string Hectoliter
        {
            get { return mHectoliter; }
            set
            {
                if (value == mHectoliter) return;
                mHectoliter = value;
                NotifyOfPropertyChange(() => Hectoliter);
                SetUnitAbbreviation("hl");
            }
        }

        public string Millimeter
        {
            get { return mMillimeter; }
            set
            {
                if (value == mMillimeter) return;
                mMillimeter = value;
                NotifyOfPropertyChange(() => Millimeter);
                SetUnitAbbreviation("mm");
            }
        }

        public string Centimeter
        {
            get { return mCentimeter; }
            set
            {
                if (value == mCentimeter) return;
                mCentimeter = value;
                NotifyOfPropertyChange(() => Centimeter);
                SetUnitAbbreviation("cm");
            }
        }

        public string Meter
        {
            get { return mMeter; }
            set
            {
                if (value == mMeter) return;
                mMeter = value;
                NotifyOfPropertyChange(() => Meter);
                SetUnitAbbreviation("m");
            }
        }

        public string Kilometer
        {
            get { return mKilometer; }
            set
            {
                if (value == mKilometer) return;
                mKilometer = value;
                NotifyOfPropertyChange(() => Kilometer);
                SetUnitAbbreviation("km");
            }
        }

        public string Squaremillimeter
        {
            get { return mSquaremillimeter; }
            set
            {
                if (value == mSquaremillimeter) return;
                mSquaremillimeter = value;
                NotifyOfPropertyChange(() => Squaremillimeter);
                SetUnitAbbreviation("qmm");
            }
        }

        public string Squarecentimeter
        {
            get { return mSquarecentimeter; }
            set
            {
                if (value == mSquarecentimeter) return;
                mSquarecentimeter = value;
                NotifyOfPropertyChange(() => Squarecentimeter);
                SetUnitAbbreviation("qcm");
            }
        }

        public string Squaremeter
        {
            get { return mSquaremeter; }
            set
            {
                if (value == mSquaremeter) return;
                mSquaremeter = value;
                NotifyOfPropertyChange(() => Squaremeter);
                SetUnitAbbreviation("qm");
            }
        }

        public string Squarekilometer
        {
            get { return mSquarekilometer; }
            set
            {
                if (value == mSquarekilometer) return;
                mSquarekilometer = value;
                NotifyOfPropertyChange(() => Squarekilometer);
                SetUnitAbbreviation("qkm");
            }
        }

        public string Hectar
        {
            get { return mHectar; }
            set
            {
                if (value == mHectar) return;
                mHectar = value;
                NotifyOfPropertyChange(() => Hectar);
                SetUnitAbbreviation("ha");
            }
        }

        public string Milligram
        {
            get { return mMilligram; }
            set
            {
                if (value == mMilligram) return;
                mMilligram = value;
                NotifyOfPropertyChange(() => Milligram);
                SetUnitAbbreviation("mg");
            }
        }

        public string Gram
        {
            get { return mGram; }
            set
            {
                if (value == mGram) return;
                mGram = value;
                NotifyOfPropertyChange(() => Gram);
                SetUnitAbbreviation("g");
            }
        }

        public string Kilogram
        {
            get { return mKilogram; }
            set
            {
                if (value == mKilogram) return;
                mKilogram = value;
                NotifyOfPropertyChange(() => Kilogram);
                SetUnitAbbreviation("kg");
            }
        }

        public string Ton
        {
            get { return mTon; }
            set
            {
                if (value == mTon) return;
                mTon = value;
                NotifyOfPropertyChange(() => Ton);
                SetUnitAbbreviation("t");
            }
        }

        public string FluidOunce
        {
            get { return mFluidOunce; }
            set
            {
                if (value == mFluidOunce) return;
                mFluidOunce = value;
                NotifyOfPropertyChange(() => FluidOunce);
                SetUnitAbbreviation("floz");
            }
        }

        public string Gill
        {
            get { return mGill; }
            set
            {
                if (value == mGill) return;
                mGill = value;
                NotifyOfPropertyChange(() => Gill);
                SetUnitAbbreviation("gi");
            }
        }

        public string Pint
        {
            get { return mPint; }
            set
            {
                if (value == mPint) return;
                mPint = value;
                NotifyOfPropertyChange(() => Pint);
                SetUnitAbbreviation("pt");
            }
        }

        public string Quart
        {
            get { return mQuart; }
            set
            {
                if (value == mQuart) return;
                mQuart = value;
                NotifyOfPropertyChange(() => Quart);
                SetUnitAbbreviation("qt");
            }
        }

        public string Gallon
        {
            get { return mGallon; }
            set
            {
                if (value == mGallon) return;
                mGallon = value;
                NotifyOfPropertyChange(() => Gallon);
                SetUnitAbbreviation("gal");
            }
        }

        public string Grain
        {
            get { return mGrain; }
            set
            {
                if (value == mGrain) return;
                mGrain = value;
                NotifyOfPropertyChange(() => Grain);
                SetUnitAbbreviation("gr");
            }
        }

        public string Drachm
        {
            get { return mDrachm; }
            set
            {
                if (value == mDrachm) return;
                mDrachm = value;
                NotifyOfPropertyChange(() => Drachm);
                SetUnitAbbreviation("dr");
            }
        }

        public string Ounce
        {
            get { return mOunce; }
            set
            {
                if (value == mOunce) return;
                mOunce = value;
                NotifyOfPropertyChange(() => Ounce);
                SetUnitAbbreviation("oz");
            }
        }

        public string Pound
        {
            get { return mPound; }
            set
            {
                if (value == mPound) return;
                mPound = value;
                NotifyOfPropertyChange(() => Pound);
                SetUnitAbbreviation("lb");
            }
        }

        public string Stone
        {
            get { return mStone; }
            set
            {
                if (value == mStone) return;
                mStone = value;
                NotifyOfPropertyChange(() => Stone);
                SetUnitAbbreviation("st");
            }
        }

        public string HundredWeight
        {
            get { return mHundredWeight; }
            set
            {
                if (value == mHundredWeight) return;
                mHundredWeight = value;
                NotifyOfPropertyChange(() => HundredWeight);
                SetUnitAbbreviation("cwt");
            }
        }

        public string ImperialTon
        {
            get { return mImperialTon; }
            set
            {
                if (value == mImperialTon) return;
                mImperialTon = value;
                NotifyOfPropertyChange(() => ImperialTon);
                SetUnitAbbreviation("it");
            }
        }

        public string Squarefoot
        {
            get { return mSquarefoot; }
            set
            {
                if (value == mSquarefoot) return;
                mSquarefoot = value;
                NotifyOfPropertyChange(() => Squarefoot);
                SetUnitAbbreviation("sft");
            }
        }

        public string Perch
        {
            get { return mPerch; }
            set
            {
                if (value == mPerch) return;
                mPerch = value;
                NotifyOfPropertyChange(() => Perch);
                SetUnitAbbreviation("perch");
            }
        }

        public string Rood
        {
            get { return mRood; }
            set
            {
                if (value == mRood) return;
                mRood = value;
                NotifyOfPropertyChange(() => Rood);
                SetUnitAbbreviation("rood");
            }
        }

        public string Acre
        {
            get { return mAcre; }
            set
            {
                if (value == mAcre) return;
                mAcre = value;
                NotifyOfPropertyChange(() => Acre);
                SetUnitAbbreviation("acre");
            }
        }

        public string Though
        {
            get { return mThough; }
            set
            {
                if (value == mThough) return;
                mThough = value;
                NotifyOfPropertyChange(() => Though);
                SetUnitAbbreviation("th");
            }
        }

        public string Inch
        {
            get { return mInch; }
            set
            {
                if (value == mInch) return;
                mInch = value;
                NotifyOfPropertyChange(() => Inch);
                SetUnitAbbreviation("in");
            }
        }

        public string Foot
        {
            get { return mFoot; }
            set
            {
                if (value == mFoot) return;
                mFoot = value;
                NotifyOfPropertyChange(() => Foot);
                SetUnitAbbreviation("ft");
            }
        }

        public string Yard
        {
            get { return mYard; }
            set
            {
                if (value == mYard) return;
                mYard = value;
                NotifyOfPropertyChange(() => Yard);
                SetUnitAbbreviation("yd");
            }
        }

        public string Chain
        {
            get { return mChain; }
            set
            {
                if (value == mChain) return;
                mChain = value;
                NotifyOfPropertyChange(() => Chain);
                SetUnitAbbreviation("ch");
            }
        }

        public string Furlong
        {
            get { return mFurlong; }
            set
            {
                if (value == mFurlong) return;
                mFurlong = value;
                NotifyOfPropertyChange(() => Furlong);
                SetUnitAbbreviation("fur");
            }
        }

        public string Mile
        {
            get { return mMile; }
            set
            {
                if (value == mMile) return;
                mMile = value;
                NotifyOfPropertyChange(() => Mile);
                SetUnitAbbreviation("mI");
            }
        }

        public string League
        {
            get { return mLeague; }
            set
            {
                if (value == mLeague) return;
                mLeague = value;
                NotifyOfPropertyChange(() => League);
                SetUnitAbbreviation("lea");
            }
        }

        public string Fathom
        {
            get { return mFathom; }
            set
            {
                if (value == mFathom) return;
                mFathom = value;
                NotifyOfPropertyChange(() => Fathom);
                SetUnitAbbreviation("ftm");
            }
        }

        protected string RadioButtonGroupName { get; set; }

        public void SetUnitAbbreviation(string abbreviation)
        {
            mArguments.UnitForConversion = abbreviation;
        }
    }
}