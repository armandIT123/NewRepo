﻿using Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using The49.Maui.BottomSheet;

namespace EcoFarm;

public class ProductBottomSheetViewModel : DataContextBase
{
    private Product product;
    private int quantity = 1;
    private string buttonTextTemplate = "Adaugă în coș - {0} lei";

    public ProductBottomSheetViewModel()
	{
			
	}

    public byte[] Image => product?.Image;
    public string Name => product?.Name;
    public string Description => product?.Description;
    public double Price => product?.Price ?? 0;

    public int Quantity
    {
        get => quantity;
        set
        {
            quantity = value;
            
            OnPropertyChanged();
            OnPropertyChanged(nameof(ButtonText));
            OnPropertyChanged(nameof(IsDecreaseQuantityBtnEnabled));
        }
    }

    public string ButtonText => string.Format(buttonTextTemplate, Math.Round(quantity * Price, 2));

    public bool IsDecreaseQuantityBtnEnabled => quantity > 1;

    public ICommand ChangeQuantity => new CommandHelper<string>((param) =>
    {
        Quantity += param == "+" ? 1 : -1;
    });

    public void DisplayProduct(Product product)
    {
        this.product = product;
        Quantity = 1;
        OnPropertyChanged(nameof(Image));
        OnPropertyChanged(nameof(Name));
        OnPropertyChanged(nameof(Description));
        OnPropertyChanged(nameof(Price));
    }
}

public partial class ProductBottomSheet : BottomSheet, INotifyPropertyChanged
{
	private ProductBottomSheetViewModel viewModel;

	public ProductBottomSheet()
	{
		InitializeComponent();
        viewModel = new ProductBottomSheetViewModel();
        BindingContext = viewModel;
        HasBackdrop = true;
		CornerRadius = 15;
	}

    public void DisplayProduct(Product product)
    {
        viewModel.DisplayProduct(product);
    }

    private void CloseButton_Clicked(object sender, EventArgs e)
    {
        this.DismissAsync();
    }
}