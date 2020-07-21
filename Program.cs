/*
 * Created by SharpDevelop.
 * User: 1
 * Date: 08.07.2020
 * Time: 16:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 * */
using System;

namespace projectPract
{
	class Program
	{
		
			const double G = 9.81; //ускорение свободного падения является постоянным значением	
	
			public static void Main(string[] args)
		     {      
			 			
			 		Console.WriteLine("Введём начальные данные, необходимые для расчёта");
			 		
			 		Console.WriteLine("Введите шероховатость трубы, м");
			 		double pipeRoughnessM = Convert.ToDouble(Console.ReadLine());
			 		Console.ReadKey(true);
			 		
			 		Console.WriteLine("Введите диаметр трубы, м");
			 		double pipeDiameterM = Convert.ToDouble(Console.ReadLine());
			 		Console.ReadKey(true);
			 		
			 		Console.WriteLine("Введите длину трубы, м");
			 		double pipeLenghtM = Convert.ToDouble(Console.ReadLine());
			 		Console.ReadKey(true);
			 		
			 		Console.WriteLine("Введите высоту трубы, м");
			 		double pipeHeightM = Convert.ToDouble(Console.ReadLine());
			 		Console.ReadKey(true);
			 		
			 		Console.WriteLine("Введите объёмный расход жидкости , м3 / с");
			 		double volRateLiqM3perS = Convert.ToDouble(Console.ReadLine());
			 		Console.ReadKey(true);
			 		
			 		Console.WriteLine("Введите объёмный расход нефти, Па");
			 		double volRateOilM3perS = Convert.ToDouble(Console.ReadLine());
			 		Console.ReadKey(true);
			 		
			 		Console.WriteLine("Введите газовый фактор , м3 / м3");
			 		double gasFactor = Convert.ToDouble(Console.ReadLine());
			 		Console.ReadKey(true);
			 		
			 		Console.WriteLine("Введите среднюю темпуратуру , К");
			 		double averTemperatureK = Convert.ToDouble(Console.ReadLine());
			 		Console.ReadKey(true);
			 		
			 		Console.WriteLine("Введите начальную температуру, К");
			 		double beginTemperature = Convert.ToDouble(Console.ReadLine());
			 		Console.ReadKey(true);
			 		
			 		Console.WriteLine("Введите плотность дегазированной нефти, кг / м3");
			 		double oilDensityKgPerM3 = Convert.ToDouble(Console.ReadLine());
			 		Console.ReadKey(true);
			 		
			 		Console.WriteLine("Введите относительную плотность (по воздуху)");
			 		double relativeDensity = Convert.ToDouble(Console.ReadLine());
			 		Console.ReadKey(true);
			 		
			 		Console.WriteLine("Введите плотность пластовой воды, кг / м3");
			 		double liqDensityKgPerM3 = Convert.ToDouble(Console.ReadLine());
			 		Console.ReadKey(true);
			 		
			 		Console.WriteLine("Введите динамическую вязкость нефти, кг / м / с");
			 		double oilViscosityKgPerMs = Convert.ToDouble(Console.ReadLine());
			 		Console.ReadKey(true);
			 		
			 		Console.WriteLine("Введите динамическую вязкость воды, кг / м / с");
			 		double waterViscosityKgPerM3 = Convert.ToDouble(Console.ReadLine());
			 		Console.ReadKey(true);
			 		
			 		Console.WriteLine("Введите давление в начале трубы, Па");
			 		double pressureInitPa = Convert.ToDouble(Console.ReadLine());
			 		Console.ReadKey(true);
			 		
			 		Console.WriteLine("Введите плотность газа, кг / м3");
			 		double gasDensityKgPerM3 = Convert.ToDouble(Console.ReadLine());
			 		Console.ReadKey(true);
			 		
			 		Console.WriteLine("Введите вязкость газа, кг / м3");
			 		double gasViscosityKgPerMs = Convert.ToDouble(Console.ReadLine());
			 		Console.ReadKey(true);
			 					 		
			 		
			 		double endPressurePa = GetPressurePa(pipeRoughnessM, pipeDiameterM,
						pipeLenghtM, pipeHeightM, volRateLiqM3perS, volRateOilM3perS,
						gasFactor, averTemperatureK, beginTemperature, oilDensityKgPerM3, 
						relativeDensity, liqDensityKgPerM3, gasDensityKgPerM3, oilViscosityKgPerMs,
						waterViscosityKgPerM3, gasViscosityKgPerMs, pressureInitPa);
			 		
			 			 	
			 		Console.WriteLine("Полученное значение давления составляет {0} Па", endPressurePa);
		            Console.ReadKey(true);
			 }
			 
			 
			 
			 public static double GetPressurePa(double pipeRoughnessM, double pipeDiameterM,
					double pipeLenghtM, double pipeHeightM, double volRateLiqM3perS, double volRateOilM3perS,
					double gasFactor, double averTemperatureK, double beginTemperature, double oilDensityKgPerM3, 
					double relativeDensity, double liqDensityKgPerM3, double gasDensityKgPerM3,	double oilViscosityKgPerMs, 
					double waterViscosityKgPerM3, double gasViscosityKgPerMs, double pressureInitPa)
			 {
					double endPressurePa = 0.0;
				
			 		//рассчитаем объёмный расход газа, зная газовый фактор
			 		double volRateGasM3perS = gasFactor * volRateOilM3perS;
			 		
			 		//рассчитаем полный объёмный расход смеси
			 		double volRateM3perS = volRateLiqM3perS + volRateGasM3perS;
			 		
			 		//рассчитаем поверхностную скорость
			 		double velocityMixMperS = GetVelocityMixtureMperS(volRateM3perS, pipeDiameterM);
			 		
			 		//вязкость жидкости
			 		double liqViscosityKgPerMs = (volRateLiqM3perS - volRateOilM3perS) / volRateLiqM3perS * waterViscosityKgPerM3 + volRateOilM3perS / volRateLiqM3perS * oilViscosityKgPerMs;
			 		
			 		//получаем чилсо Ренольдса для жидкости
			 		double reynoldsNumberLiq = GetReynoldsNumberLiq(velocityMixMperS, pipeDiameterM, liqDensityKgPerM3,
		 	                                       liqViscosityKgPerMs);
			 					 		
			 		//получаем число Фруде
			 		double froudeNumber = GetFroudeNumber(velocityMixMperS, pipeDiameterM); //Определяется число Фруда
			 		
			 			
			 		//далее вычислим объёмные концентрации газожидкостной смеси
			 		double liqFract = volRateLiqM3perS / volRateM3perS;
			 		double gasFract = 1 - liqFract;
			 		
			 		//вычислим плотность данной смеси
			 		double fluidDensityKgPerM3 = GetFluidDensityKgPerM3(liqDensityKgPerM3, gasDensityKgPerM3,
			 		                                                    liqFract, gasFract);
			 		
			 		
			 		//определим газовое число
			 		double gasNumber = GetGasNumber(liqFract, gasFract);
			 		
			 		if(GetFlowRegime(gasNumber, froudeNumber, reynoldsNumberLiq, pipeHeightM, pipeLenghtM))
			 		{
			 				//если пробковый режим, то вызываем расчётчик перапада давления для пробкового режима
			 				endPressurePa = PressureDropSlugRegime.GetPressureDropForSlugRegime(liqDensityKgPerM3, gasDensityKgPerM3, 
		                                                    fluidDensityKgPerM3, gasNumber, froudeNumber, gasFract, velocityMixMperS, 
		                                             		liqViscosityKgPerMs, gasViscosityKgPerMs, pipeDiameterM, pipeRoughnessM,
		                                             		pipeLenghtM, pipeHeightM);
			 				
			 		}
			 		else
			 		{
			 				//если расслоенный режимим, то вызываем расчётчик перапада давления для расслоенного режима
			 				endPressurePa = PressureDropForIntermRegime.GetPressureDropForIntermRegime(liqDensityKgPerM3, gasDensityKgPerM3, 
		                                                    fluidDensityKgPerM3, gasNumber, froudeNumber, velocityMixMperS, liqViscosityKgPerMs, 
		                                                    gasViscosityKgPerMs, pipeDiameterM, pipeRoughnessM, pipeLenghtM, pipeHeightM,
		                                            		gasFract, liqFract, reynoldsNumberLiq);
			 				
			 		}
			 		
			 		return endPressurePa;
			 }
			 
			 
		 
		 	 public static double GetVelocityMixtureMperS(double volRateM3PerS, double pipeDiameterM)//Определяется скорость газожидкостной смеси
		 	 {
		 	 	return 4 * volRateM3PerS / Math.PI / Math.Pow(pipeDiameterM, 2);
		 	 }
	 	 
		 	 public static double GetReynoldsNumberLiq(double velocityMixtureMperS, double pipeDiameterM, double liqDensityKgPerM3,
		 	                                       double liqViscosityKgPerMs)//Определяется число Рейнольдса для жидкой фазы
		 	 {
		 			return velocityMixtureMperS * pipeDiameterM * liqDensityKgPerM3 / liqViscosityKgPerMs;
		 	 }
	 	 
		  	 public static double GetFroudeNumber(double velocityMixtureMperS, double pipeDiameterM)//Определяется число Фруда
		  	 {
		  	 		return velocityMixtureMperS * velocityMixtureMperS / G / pipeDiameterM;
		  	 }
	  	 
		  	 public static double GetFluidDensityKgPerM3(double liqDensityKgPerM3, double gasDensityKgPerM3,
		  	                                          double betta1, double betta2)//Определяется расходная плотность газожидкостной смеси
		  	//β1 и β2 – расходные объемные концентрации жидкой и газовой фаз соответственно
		  	//        – плотность свободного газа в рабочих условиях
		 	{
		  	 		return betta1 * liqDensityKgPerM3 + betta2 * gasDensityKgPerM3;
		 	}
	  	 
		  	public static double GetGasNumber(double betta1, double betta2)//Определяется синус угла наклона трубы к горизонту
		  	{
		  			return betta2 / betta1;
		  	}
		  	
		  	
		  	//определение режима течения (тут их всего два)
		  	public static bool GetFlowRegime(double gasNumber, double froudeNumber, 
		  	                                double reynoldsNumberLiq, double pipeHeightM,
		  	                               double pipeLengthM)//Определяется синус угла наклона трубы к горизонту
		  	{
		  		double A = 0.13 * (1 + gasNumber) * (froudeNumber * Math.Pow(reynoldsNumberLiq, -0.2) + 0.0006) + Math.Sin(pipeHeightM / pipeLengthM);
		  		
		  		if(A >= 0)
		  		{
		  			Console.Write("Пробковый режим");
		  			return true; //пробковый режим
		  			
		  		}
		  		else 
		  		{
		  			Console.Write("Расслоенный режим");
		  			return false; //расслоенный режим
		  		}
		  				
		  	}
	  	
	}
	/// <summary>
	/// Description of PressureDropForIntermRegime.
	/// </summary>
	public static class PressureDropForIntermRegime
	{
		const double G = 9.81;
		
		public static double[] GetVolumeConcGasAndLiq(double gasNumber, double froudeNumber, 
		                                             double volConcGasPhase)// концентрация газа и жидкости
		{
			 var volConcentrates = new double[2];
			 
			 //истинная концентрация газовой фазы
			 volConcentrates[1] = 1 - (1 - volConcGasPhase) * (1 + 0.5 * Math.Pow(gasNumber, 0.85) * Math.Exp(-0.052 * Math.Pow(froudeNumber, 0.6) * gasNumber));
			 
			 //истинная концентрация жидкой фазы
			 volConcentrates[0] = 1 - volConcentrates[0];
			 
			 return volConcentrates;
		}
		
		
		public static double GetPressureDropForIntermRegime(double liqDensityKgPerM3, double gasDensityKgPerM3, 
		                                                    double fluidDensituKgPerM3,
		                                                    double gasNumber, double froudeNumber,
		                                              double velocityMixtureMperS, 
		                                             double liqViscosityKgPerMs, double gasViscosityKgPerMs, 
		                                             double pipeDiameterM, double pipeRoughnessM,
		                                            double pipeLengthM, double pipeHeightM,
		                                           double volConcGas, double volConcLiq, double reynoldsNumberLiq)
		{
									
			double reynoldsNumberGas = gasDensityKgPerM3 * volConcGas * velocityMixtureMperS * pipeDiameterM / 
				gasViscosityKgPerMs;
			
			double coefHydrResistGas = 0.1 * Math.Pow(pipeRoughnessM / pipeDiameterM + 45 / reynoldsNumberGas, 0.2);
						
			double b0 = 0.429 * Math.Pow(froudeNumber, 0.23) - 0.039 * Math.Pow(froudeNumber, 0.63);
			double c0 = 0.026 * Math.Pow(froudeNumber, 0.63);
				
			double K = Math.Pow(1 - 0.34 * Math.Pow(froudeNumber, 0.4) / Math.Pow(reynoldsNumberLiq, 0.8) / Math.Pow(0.3 * volConcGas / volConcLiq / (25.4 * Math.Pow(froudeNumber, -0.53)) - Math.Sin(pipeHeightM / pipeLengthM), 0.4), -2.5)
				+ 5.5 * (froudeNumber + 0.05) * Math.Pow(volConcGas / volConcLiq, b0) * Math.Exp(-c0 * volConcGas / volConcLiq);
				
			double pressureDropPa = coefHydrResistGas * K * pipeLengthM * gasDensityKgPerM3 * volConcGas / volConcLiq * Math.Pow(velocityMixtureMperS, 2) / 2 / pipeDiameterM 
				+ gasDensityKgPerM3 * G * pipeLengthM * Math.Sin(pipeHeightM / pipeLengthM);
					
			return pressureDropPa;
		}
	}
	/// <summary>
	/// Description of PressureDropSlugRegime.
	/// </summary>
	public static class PressureDropSlugRegime
	{
		const double G = 9.81;
		
		
		public static double[] GetVolumeConcGasAndLiq(double gasNumber, double froudeNumber, 
		                                             double volConcGasPhase)// концентрация газа и жидкости
		{
			 var volConcentrates = new double[2];
			 
			 //истинная концентрация газовой фазы
			 volConcentrates[1] = 1 - (1 - volConcGasPhase) * (1 + 0.5 * Math.Pow(gasNumber, 0.85) * Math.Exp(-0.052 * Math.Pow(froudeNumber, 0.6) * gasNumber));
			 
			 //истинная концентрация жидкой фазы
			 volConcentrates[0] = 1 - volConcentrates[0];
			 
			 return volConcentrates;
		}
		
		
		public static double GetPressureDropForSlugRegime(double liqDensityKgPerM3, double gasDensityKgPerM3, 
		                                                    double fluidDensityKgPerM3,
		                                                    double gasNumber, double froudeNumber,
		                                             double volConcGasPhase, double velocityMixtureMperS, 
		                                             double liqViscosityKgPerMs, double gasViscosityKgPerMs, 
		                                             double pipeDiameterM, double pipeRoughnessM,
		                                            double pipeLengthM, double pipeHeightM)
		{
			double psi = 0.0;
			double coefHydrResist = 0.0;//привидённый коэффициент гидравлического сопротивления		
			
			var volConcentrates = GetVolumeConcGasAndLiq(gasNumber, froudeNumber, 
		                                             volConcGasPhase);
						
			double reynoldsNumberSlug = fluidDensityKgPerM3 * velocityMixtureMperS * pipeDiameterM / 
				(liqViscosityKgPerMs * volConcentrates[0] + gasViscosityKgPerMs * volConcentrates[1]);
						
			double a3 = 0.0075 / (froudeNumber + 0.0005);
			double b3 = 0.22;
			double c3 = 0.0575 * Math.Pow(froudeNumber, 0.21);
				
			if(froudeNumber >= 0.35)
				psi = 1.0;
			else
				psi = a3 * Math.Pow(gasNumber, b3) * Math.Exp(-c3 * gasNumber);
			
			
			if(reynoldsNumberSlug >= 1500)
				coefHydrResist = 0.1 * Math.Pow(pipeRoughnessM / pipeDiameterM + 45 / reynoldsNumberSlug, 0.2);
			else
				coefHydrResist = 64 / reynoldsNumberSlug;
			
			double coeffMixHydrResist = coefHydrResist * psi;
			
			double properDensityKgPerM3 = volConcentrates[0] * liqDensityKgPerM3 + volConcentrates[1] * gasDensityKgPerM3;
			
			double pressureDropPa = coeffMixHydrResist * pipeLengthM * fluidDensityKgPerM3 * Math.Pow(velocityMixtureMperS, 2) / 2 / pipeDiameterM 
				+ properDensityKgPerM3 * G * pipeLengthM * Math.Sin(pipeHeightM / pipeLengthM);
					
			return pressureDropPa;
		}		
	}
}
