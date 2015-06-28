---
content-type: markdown
---

If you want to execute some arbritrary piece of code regardless of how a task exited, you can use the `Finally` task extension.

	Task("A")
	  .Does(() =>
	{
	})
	.Finally(() =>
	{  
		// Do magic.
	});