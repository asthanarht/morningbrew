package md52d189d64b7b0ac40b59b56928c06114b;


public class ToastCallback
	extends android.support.design.widget.Snackbar.Callback
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDismissed:(Landroid/support/design/widget/Snackbar;I)V:GetOnDismissed_Landroid_support_design_widget_Snackbar_IHandler\n" +
			"";
		mono.android.Runtime.register ("Plugin.Toasts.ToastCallback, Toasts.Forms.Plugin.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ToastCallback.class, __md_methods);
	}


	public ToastCallback ()
	{
		super ();
		if (getClass () == ToastCallback.class)
			mono.android.TypeManager.Activate ("Plugin.Toasts.ToastCallback, Toasts.Forms.Plugin.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onDismissed (android.support.design.widget.Snackbar p0, int p1)
	{
		n_onDismissed (p0, p1);
	}

	private native void n_onDismissed (android.support.design.widget.Snackbar p0, int p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
