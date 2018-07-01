# .NET Core implementation of Gaussian Processes

### Objective function exploration:
```
var kernel = new GaussianKernel(0.25, 1);
var model = new Model(kernel, 0, 8, 800, ObjectiveFunction);
var output = model.Explore(14);
```
![Alt Text](https://github.com/koryakinp/GP/blob/master/GP/gifs/gp-explore.gif?raw=true)
### Objective function exploration:
```
var kernel = new GaussianKernel(0.25, 1);
var model = new Model(kernel, 0, 8, 800, ObjectiveFunction);
var output = model.FindExtrema(Goal.Max, 14);
```
![Alt Text](https://github.com/koryakinp/GP/blob/master/GP/gifs/gp-max-ei.gif?raw=true)
