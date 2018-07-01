# Implementation of Gaussian Processes using .NET Core

### Objective function approximation:

![Alt Text](https://github.com/koryakinp/GP/blob/master/GP/gp-explore.gif?raw=true)

```
var kernel = new GaussianKernel(0.25, 1);
var model = new Model(kernel, 0, 8, 800, Math.Sin);
var output = model.Explore(10);
```
