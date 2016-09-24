Time <- read.csv("~/Documents/R finale/Time.csv", sep=";")

par(mfrow=c(1,2))

colors <- rainbow(3) 
linetype <- c(1:3) 
plotchar <- seq(18,18+3,1)

colors <- rainbow(3) 
linetype <- c(1:3) 
plotchar <- seq(18,18+3,1)

TimeManual<-Time[1:6,]
TimeHHD<-Time[7:12,]
TimeHMD<-Time[13:18,]

TimeManualMeanForTask<-colMeans(TimeManual)
TimeHHDMeanForTask<-colMeans(TimeHHD)
TimeHMDMeanForTask<-colMeans(TimeHMD)

maxMean<-max(TimeManualMeanForTask,TimeHHDMeanForTask,TimeHMDMeanForTask)

# set up the plot
plot(c(1,20),c(10,maxMean),type="n",xlab="Task number",ylab="Tempo medio",main = "Tempo medio completamento singoli task per gruppo",xaxt = "n")
axis(1, at=1:20, labels=3:22,cex.axis=0.8)

#plot line
lines(1:20, TimeManualMeanForTask, type="b", lwd=1.5,
      lty=linetype[1], col=colors[1], pch=plotchar[1]) 

lines(1:20, TimeHHDMeanForTask, type="b", lwd=1.5,
      lty=linetype[2], col=colors[2], pch=plotchar[2]) 

lines(1:20, TimeHMDMeanForTask, type="b", lwd=1.5,
      lty=linetype[3], col=colors[3], pch=plotchar[3]) 

legend(range(12:22), range(140:195), c("Manuale tradizionale","HHD AR","HMD AR"), cex=0.8, col=colors,
       pch=plotchar, lty=linetype)

summary(TimeManualMeanForTask)
summary(TimeHHDMeanForTask)
summary(TimeHMDMeanForTask)

#anova faccio la media dei task per ogni esperimento
TimeManualMeanForExperiment<-rowMeans(TimeManual)
TimeHHDMeanForExperiment<-rowMeans(TimeHHD)
TimeHMDMeanForExperiment<-rowMeans(TimeHMD)

TimeMeanForExperiment<-NULL
#li concateno
TimeMeanForExperiment<-data.frame(c(TimeManualMeanForExperiment,TimeHHDMeanForExperiment,TimeHMDMeanForExperiment))

#assegno label ad ogni esperimento
myClass<-c(rep("A",6),rep("B",6),rep("C",6))

TimeMeanForExperiment<-data.frame(TimeMeanForExperiment,myClass)

#ipotesi normalità
shapiro.test(TimeManualMeanForExperiment)

shapiro.test(TimeHHDMeanForExperiment)

shapiro.test(TimeHMDMeanForExperiment)

#ipotesi variaza

bartlett.test(TimeMeanForExperiment[1:18,1] ~ myClass,data=TimeMeanForExperiment)

boxplot(TimeMeanForExperiment[1:18,1] ~ myClass[1:18], data=TimeMeanForExperiment,main="Boxplot tempi medi task",names=c("Manuale tradizionale","HHD AR","HMD AR"),col = colors,ylab="Tempo medio")

results = aov(TimeMeanForExperiment[1:18,1] ~ myClass, data=TimeMeanForExperiment)

summary(results)

#metodo non parametrico

kruskal.test(TimeMeanForExperiment[1:18,1] ~ myClass, data=TimeMeanForExperiment) 

plot(results)
