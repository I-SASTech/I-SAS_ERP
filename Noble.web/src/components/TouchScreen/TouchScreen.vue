<template>
    <div v-if="isValid('TouchInvoiceTemplate1')">
        <div class="page-wrapper m-0 bg-light" v-if="isDayAlreadyStart">
            <!-- Top Bar Start -->
            <div class="topbar">
                <!-- Navbar -->
                <nav class="navbar-custom">
                    <ul class="list-unstyled topbar-nav float-end mb-0 touch-topbar-nav">
                        <li class="displayHidden">
                            <a class="nav-link nav-user" href="javascript:void(0);" style="height:52px;">
                                <input type="text"  v-model="barCode" @change="onBarcodeScanned(barCode)" placeholder="Search Bar Code Here" class="form-control" style="margin-top: 9px;" />
                            </a>
                        </li>
                        <li class="displayHidden">
                            <a class="nav-link nav-user" href="javascript:void(0);" v-on:click="DayEnd">
                                <span class="nav-user-name">{{ $t('Sale.User1') }}: {{ loginUserName }}</span>
                            </a>
                        </li>
                        <li class="displayHidden">
                            <a class="nav-link nav-user" href="javascript:void(0);">
                                <span class="ms-1 nav-user-name">{{ sale.date }}</span>
                            </a>
                        </li>
                        <li>
                            <a class="nav-link nav-user" href="javascript:void(0);">
                                <span class="ms-1 nav-user-name" v-on:click="openWalkModel"
                                      v-if="isValid('CanSelectCustomer')">{{ sale.cashCustomer }}</span>
                                <span class="ms-1 nav-user-name" v-else>{{ sale.cashCustomer }}</span>
                            </a>
                        </li>
                        <li>
                            <a class="nav-link nav-user" href="javascript:void(0);">
                                <span class="ms-1 nav-user-name">Invoice No: {{ sale.registrationNo }}</span>
                            </a>
                        </li>
                    </ul>
                    <!--end topbar-nav-->

                    <ul class="list-unstyled topbar-nav mb-0 displayHidden">
                        <li class="creat-btn">
                            <div class="nav-link">
                                <a class="btn btn-sm btn-soft-danger" v-on:click="goToSale" href="javascript:void(0);">
                                    {{
                                    $t('TouchScreen.Close')
                                    }}
                                </a>
                            </div>
                        </li>
                        <li class="creat-btn">
                            <div class="nav-link">
                                <a class="btn btn-sm btn-soft-primary" href="javascript:void(0);" data-bs-toggle="offcanvas"
                                   data-bs-target="#offcanvasWithBackdrop" aria-controls="offcanvasWithBackdrop">
                                    {{
                                        $t('TouchScreen.ReturnInvoice')
                                    }}
                                </a>
                            </div>
                        </li>
                    </ul>
                </nav>

                <div class="offcanvas offcanvas-start" tabindex="-1" id="offcanvasWithBackdrop"
                     aria-labelledby="offcanvasWithBackdropLabel">
                    <div class="offcanvas-header">
                        <h5 class="offcanvas-title mt-0" id="offcanvasWithBackdropLabel">
                            {{
 $t('TouchScreen.ReturnInvoice')
                            }}
                        </h5>
                        <button v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? '' : 'margin-left:0px !important'"
                                type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas"
                                aria-label="Close"></button>
                    </div>
                    <div class="offcanvas-body">
                        <div class="row">
                            <div class="col-md-12 form-group ">
                                <div class="input-group my-1">
                                    <button class="btn btn-soft-primary" type="button" id="product-search">
                                        <i class="fa fa-search"></i>
                                    </button>
                                    <input v-model="searchReturnInvoice" v-shortkey.avoid type="text"
                                           v-bind:placeholder="$t('TouchScreen.SearchInvoice')" class="form-control"
                                           aria-describedby="product-search" />
                                </div>
                            </div>
                        </div>

                        <div v-for="item in saleList" v-bind:key="item.id + 9" class="row">
                            <div class="col-12 d-grid">
                                <button type="button" class="btn btn-light mb-2" v-on:click="getPostInvoiceDetail(item.id)"
                                        data-bs-dismiss="offcanvas" aria-label="Close">
                                    <div class="width ellipse two-lines">
                                        {{ item.registrationNumber }}
                                    </div>
                                    <div class="width ellipse two-lines">
                                        {{
 item.cashCustomer != '' && item.cashCustomer != null ? item.cashCustomer :
                                            item.customerName
                                        }}
                                    </div>
                                    <div class="mt-1">
                                        {{ getDate(item.date) }}
                                    </div>
                                </button>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <!-- Top Bar End -->
            <!-- Page Content-->
            <div class="page-content pb-0">
                <div class="container-fluid">
                    <!-- Page-Title -->
                    <!--end page title end breadcrumb-->
                    <div class="row" style="height:90vh;">
                        <div class="col-md-6">
                            <div class="card mb-0 border-0 mt-1" style="height:90vh;">
                                <div class="card-header">
                                    <div class="container m-0 p-0">
                                        <div class="row">
                                            <div class="col-md-12 col-lg-12">
                                                <carousel :per-page="4" :items-to-show="3" :wrap-around="true" :paginationEnabled="false">
                                                    <slide v-for="(item, index) in categorylist" :data-index="index"
                                                           :key="item.id" :data-name="item.name">
                                                        <button @click="getProduct(item.id)" type="button" class="btn btn-light mb-3 shadow-sm">
                                                            <img src="/assets/images/small/ex-card.png" alt=""
                                                                 class="img-fluid">
                                                            <div class="ellipse two-lines">
                                                                {{ item.name }}
                                                            </div>
                                                        </button>

                                                    </slide>
                                                </carousel>

                                            </div>
                                            <div class="col-md-12 col-12">
                                                <input v-model="search" v-shortkey.avoid class="form-control"
                                                       v-bind:placeholder="$t('TouchScreen.SearchItem')" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body overflow-auto" style="height: 100%; max-height: 100%;" id="style-2">
                                    <div class="row">
                                        <div class="col-md-4 col-4 d-grid" v-for="item in produc" :key="item.id + 23"
                                             v-on:click="addProduct(item.id, item)">
                                            <button type="button" class="btn mb-3 p-1 shadow"
                                                    v-bind:class="styleclass == item.id ? 'btn-primary' : 'bg-light-alt'">
                                                <div class="width ellipse two-lines">
                                                    {{ item.displayName  }}
                                                </div>
                                                <div class="mt-1">
                                                    {{ $t('TouchScreen.Price') }} {{ currency }} {{ parseFloat(item.salePrice).toFixed(3).slice(0, -1) }}
                                                </div>
                                            </button>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-6" style="position:relative;height:90vh;">
                            <div class="card mb-0 border-0 mt-1">
                                <div class="card-header m-0 p-0" style="height:58vh;max-height:58vh;overflow:auto;"
                                     id="style-3">
                                    <div class="table-responsive-md">
                                        <table class="table mb-0">
                                            <thead class="bg-light-alt">
                                                <tr>
                                                    <th style="width: 5%;">#</th>
                                                    <th style="width: 28%;">
                                                        {{ $t('TouchScreen.Product') }}
                                                    </th>
                                                    <th class="text-center" style="width: 15%;">
                                                        {{ $t('TouchScreen.UnitPrice') }}
                                                    </th>
                                                    <th class="text-center" style="width: 27%;">
                                                        {{ $t('TouchScreen.Quantity') }}
                                                    </th>
                                                    <th class="text-end" style="width: 20%;">
                                                        {{ $t('TouchScreen.LineTotal') }}
                                                    </th>
                                                    <th style="width: 5%;"></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(prod, index) in saleProducts" :key="itemRender + index"
                                                    v-bind:class="{ 'alert-danger': prod.outOfStock }">
                                                    <td>{{ index + 1 }}</td>
                                                    <td>
                                                        <span>
                                                            {{prod.productName}}
                                                        </span>
                                                        <span v-if="prod.promotionId != null && prod.promotionId != undefined && prod.promotionId != ''"
                                                              class="badge rounded-pill badge-soft-primary">
                                                            {{ prod.promotionName }}
                                                        </span>
                                                        <span v-if="products.find(x => x.id == prod.productId).bundleCategory != undefined"
                                                              class="badge rounded-pill badge-soft-primary">
                                                            {{
 products.find(x => x.id ==
                                                                prod.productId).bundleCategory.buy
                                                            }} + {{
 products.find(x => x.id
        == prod.productId).bundleCategory.get
                                                            }}, ({{
 products.find(x =>
        x.id == prod.productId).bundleCategory.stockLimit -
        products.find(x => x.id ==
            prod.productId).bundleCategory.quantityOut
                                                            }})
                                                        </span>
                                                    </td>

                                                    <td v-on:dblclick="counter += 1, openmodel('unitPrice' + index)"
                                                        v-if="!changePriceDuringSale">
                                                        <decimal-to-fixed disable="(isAuthour.changePriceDuringSale && isAuthour.column==('unitPrice'+index))==true?'true':'false'"
                                                                          v-model="prod.unitPrice" v-bind:salePriceCheck="false"
                                                                          @update:modelValue="updateLineTotal(prod.unitPrice, 'unitPrice', prod)" />

                                                    </td>
                                                    <td v-else>
                                                        <decimal-to-fixed v-model="prod.unitPrice"
                                                                          v-bind:salePriceCheck="false"
                                                                          @update:modelValue="updateLineTotal(prod.unitPrice, 'unitPrice', prod)" />

                                                    </td>

                                                    <td :title="prod.currentQuantity">
                                                        <div class="btn-group" aria-label="Basic example" role="group">
                                                            <button type="button"
                                                                    v-on:click="quantityIncrease(prod.productId)"
                                                                    class="btn btn-soft-primary  shadow-none customButton ">
                                                                <i class="fas fa-plus"></i>
                                                            </button>
                                                            <input v-model="prod.quantity" type="text"
                                                                   @focus="$event.target.select()" v-shortkey.avoid
                                                                   class="form-control  text-center RemovePadding"
                                                                   aria-describedby="button-qty"
                                                                   @keyup="updateLineTotal($event.target.value, 'quantity', prod, true)" />
                                                            <button type="button"
                                                                    v-on:click="quantityDecrease(prod.productId)"
                                                                    class="btn btn-soft-primary  shadow-none customButton ">
                                                                <i class="fas fa-minus"></i>
                                                            </button>
                                                        </div>
                                                    </td>

                                                    <td class="text-end">
                                                        {{
 $formatAmount(parseFloat(prod.lineTotal).toFixed(3).slice(0, -1))
                                                        }}
                                                    </td>
                                                    <td class="text-end">
                                                        <a href="javascript:void(0);" @click="removeProduct(prod.rowId)">
                                                            <i class="las la-trash-alt text-secondary font-16"></i>
                                                        </a>
                                                    </td>
                                                </tr>

                                            </tbody>
                                        </table>
                                    </div>

                                </div>
                            </div>

                            <div class="row" style="position:absolute;width:100%;bottom:0;">
                                <div class="col-lg-12">
                                    <div class="card-footer bg-light-alt m-0 p-0">
                                        <div class="row">
                                            <div class="col-md-12 col-12">
                                                <table class="table mb-0">
                                                    <tbody>
                                                        <tr>
                                                            <td class="payment-title" style="width:30%;">
                                                                {{
                                                                $t('TouchScreen.NoItem')
                                                                }}
                                                            </td>
                                                            <td class="text-end" style="width:20%;">{{ summary.item }}</td>
                                                            <td class="payment-title" style="width:30%;">
                                                                {{
                                                                $t('TouchScreen.TotalQty')
                                                                }}
                                                            </td>
                                                            <td class="text-end" style="width:20%;">{{ summary.qty }}</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="payment-title">{{ $t('TouchScreen.Total') }}</td>
                                                            <td class="text-end">
                                                                {{
 $formatAmount(parseFloat(taxMethod
                                                                == 'Exclusive' ? summary.withDisc :
                                                                (summary.withDisc - summary.vat)).toFixed(3).slice(0, -1))
                                                                }}
                                                            </td>
                                                            <td class="payment-title">{{ $t('TouchScreen.Disc') }}</td>
                                                            <td class="text-end">
                                                                {{
 $formatAmount(parseFloat(summary.discount).toFixed(3).slice(0, -1))
                                                                }}
                                                            </td>
                                                        </tr>
                                                        <tr v-if="summary.bundleAmount > 0">
                                                            <td class="payment-title">
                                                                {{ $t('TouchScreen.BundleAmount') }}
                                                            </td>
                                                            <td class="text-end">
                                                                {{
 $formatAmount(parseFloat(summary.bundleAmount).toFixed(3).slice(0, -1))
                                                                }}
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="payment-title">{{ $t('TouchScreen.TotalVat') }}</td>
                                                            <td class="text-end">
                                                                {{
 $formatAmount((parseFloat(summary.vat) + summary.inclusiveVat).toFixed(3).slice(0, -1))
                                                                }}
                                                            </td>
                                                            <td class="payment-title">
                                                                {{ $t('TouchScreen.TotalWithVat') }}
                                                            </td>
                                                            <td class="text-end">
                                                                {{
 $formatAmount(parseFloat(summary.withVat).toFixed(3).slice(0, -1))
                                                                }}
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-6 col-6 mb-2 d-grid">
                                            <button class="btn btn-primary btn-lg" v-if="isValid('CanHoldTouchInvoices')"
                                                    v-on:click="openHoldModel"
                                                    v-bind:disabled="v$.$invalid || saleProducts.filter(x => x.unitPrice == '').length > 0 || saleProducts.filter(x => x.quantity == '' || x.quantity == 0).length > 0">
                                                <span v-if="sale.id == '00000000-0000-0000-0000-000000000000'">
                                                    {{ $t('TouchScreen.Hold') }}
                                                </span>
                                                <span v-else>

                                                    {{ $t('TouchScreen.HoldAndUpdate') }}
                                                </span>
                                            </button>
                                        </div>

                                        <div class="col-lg-6 col-6 mb-2 d-grid">
                                            <button class="btn btn-primary btn-lg" v-on:click="openUnHoldModel"
                                                    v-if="isValid('CanHoldTouchInvoices')">
                                                <span v-if="sale.id == '00000000-0000-0000-0000-000000000000'">
                                                    {{ $t('TouchScreen.UnHold') }}
                                                </span>
                                                <span v-else>
                                                    {{ $t('TouchScreen.UnHold') }}
                                                </span>
                                            </button>
                                        </div>

                                        <div class="col-lg-6 col-6 mb-2 d-grid">
                                            <button class="btn btn-success btn-lg" data-bs-toggle="offcanvas"
                                                    data-bs-target="#offcanvasTop" aria-controls="offcanvasTop"
                                                    v-if="!sale.isCredit || isValid('TouchInvoiceTemplate1')"
                                                    v-on:click="updateSummary"
                                                    v-bind:disabled="v$.$invalid || saleProducts.filter(x => x.outOfStock).length > 0 || saleProducts.filter(x => x.unitPrice == '').length > 0 || saleProducts.filter(x => x.quantity == '' || x.quantity == 0).length > 0">
                                                {{ $t('TouchScreen.Pay') }}
                                            </button>
                                        </div>

                                        <div class="col-lg-6 col-6 mb-2 d-grid">
                                            <button class="btn btn-danger btn-lg" v-on:click="Reset(false)">
                                                {{ $t('TouchScreen.Reset') }}
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="offcanvas offcanvas-top" tabindex="-1" id="offcanvasTop" aria-labelledby="offcanvasTopLabel"
                 style="height:auto;">
                <div class="offcanvas-body">
                    <div class="row" style="height:95vh;">
                        <div class="col-lg-7 col-7" style="position:relative;">
                            <div class="row" v-if="sale.salePayment.dueAmount >= 0">
                                <div class="col-lg-6 col-6 d-grid mb-2">
                                    <button class="btn btn-lg py-3"
                                            v-bind:class="sale.salePayment.paymentType == 'Cash' ? 'btn-primary' : 'btn-outline-primary'"
                                            v-on:click="isPayment(togglePayCash)">
                                        <i class="fas fa-money-bill-alt"></i> {{ $t('TouchScreen.Cash') }}
                                    </button>
                                </div>
                                <div class="col-lg-6 col-6 d-grid mb-2" v-if="cashVoucherAllow == 'true'">
                                    <button class="btn btn-lg py-3"
                                            v-bind:class="sale.salePayment.paymentType == 'CashVoucher' ? 'btn-primary' : 'btn-outline-primary'"
                                            v-on:click="isPayment(togglePayVoucher)">
                                        <i class="fas fa-money-bill-alt"></i> {{ $t('TouchScreen.Voucher') }}
                                    </button>
                                </div>
                                <div class="col-lg-6 col-6 d-grid mb-2">
                                    <button class="btn btn-lg py-3"
                                            v-bind:class="sale.salePayment.paymentType == 'Bank' ? 'btn-primary' : 'btn-outline-primary'"
                                            v-on:click="isPayment(toggleBank)">
                                        <i class="fas fa-money-bill-alt"></i> {{ $t('TouchScreen.Bank') }}
                                    </button>
                                </div>

                                <div class="col-12" v-if="sale.salePayment.paymentType == 'Bank'">
                                    <div class="row">
                                        <div class="col-xs-6 col-sm-4 col-lg-6 mt-3 d-grid" v-for="bank in bankList"
                                             v-bind:key="bank.id + 15">
                                            <button v-on:click="selectBank(bank.id, bank.name)" type="button" class="btn"
                                                    v-bind:class="bank.id == bankId ? 'btn-primary' : 'btn-light'">
                                                <div class="text-truncate" style="font-size:12px;">{{ bank.name }} </div>
                                                <img src="Product.png"
                                                     v-if="bank.image == null || bank.image == '' || bank.image == undefined"
                                                     style="width: 45px; height: 35px;">
                                                <img :src="'data:image/png;base64,' + bank.image" v-else
                                                     style="width: 55px; height: 35px;">
                                                <div class="text-truncate" style="font-size:12px;">
                                                    {{ bank.nameArabic }}
                                                </div>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 mt-2" v-if="sale.salePayment.paymentType == 'OtherCurrency'">
                                    <div class="row">
                                        <div class="col-4 text-center col-padding-remove" v-for="currency in currencyList"
                                             v-bind:key="currency.id">
                                            <div v-bind:class="{ bg_color: currency.id == otherCurrencyId }"
                                                 v-on:click="selectCurrency(currency.id, currency.name)"
                                                 style="border: 1px solid rgb(45, 118, 247); padding: 5px; border-radius: 5px;font-weight: 600;cursor: pointer;color:white;">
                                                {{ currency.sign }}
                                                <img src="Product.png"
                                                     v-if="currency.image == null || currency.image == '' || currency.image == undefined"
                                                     style="width: 40px; height: 25px;">
                                                <img :src="'data:image/png;base64,' + currency.image" v-else
                                                     style="width: 40px; height: 25px;">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-2">
                                        <div class="col-xs-6 col-sm-4 col-lg-4  col-padding-remove"
                                             v-if="sale.salePayment.paymentType == 'OtherCurrency'">
                                            <label class="text-white">{{ $t('TouchScreen.Amount') }}:</label>
                                            <div>
                                                <input type="number" class="form-control"
                                                       @focus="FocusInput('amount', $event)" @blur="FocusOutInput()"
                                                       placeholder="" @update:modelValue="OtherCurrencyCalculate()"
                                                       v-model="sale.otherCurrency.amount" />
                                            </div>
                                        </div>

                                        <div class="col-xs-6 col-sm-4 col-lg-4 col-padding-remove"
                                             v-if="sale.salePayment.paymentType == 'OtherCurrency'">
                                            <label class="text-white"> {{ $t('TouchScreen.Rate') }}:</label>
                                            <div>
                                                <input type="number" class="form-control"
                                                       @focus="FocusInput('rate', $event)" @blur="FocusOutInput()"
                                                       @update:modelValue="OtherCurrencyCalculate()" v-model="sale.otherCurrency.rate" />
                                            </div>
                                        </div>

                                        <div class="col-xs-6 col-sm-4 col-lg-4  col-padding-remove"
                                             v-if="sale.salePayment.paymentType == 'OtherCurrency'">
                                            <label class="text-white">{{ $t('TouchScreen.Total') }}:</label>
                                            <div>
                                                <input type="number" class="form-control" @focus="$event.target.select()"
                                                       disabled
                                                       :value="$roundAmount((sale.otherCurrency.rate * sale.otherCurrency.amount))" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12" v-if="sale.salePayment.paymentType == 'CashVoucher'">
                                    <div class="row">
                                        <div class="col-8 mt-3 text-center col-padding-remove">
                                            <input type="text" class="form-control" style="height: 37px;"
                                                   @focus="FocusInput('voucher', $event)" v-model="sale.voucherNo" />
                                        </div>
                                        <div class="col-4 mt-1 text-center col-padding-remove">
                                            <button @click="VerifyCashVoucher()" class="btn btn-primary btn-block">
                                                {{ $t('TouchScreen.Apply') }}
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row mt-2">
                                <div class="col-xs-6 col-sm-4 col-lg-3 ">
                                    <div class="card">
                                        <div class="card-body text-center py-2">
                                            <p class="mb-0">{{ $t('TouchScreen.DueAmount') }}</p>
                                            <h4 class="my-0">{{ sale.salePayment.dueAmount }}</h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-6 col-sm-4 col-lg-3 ">
                                    <div class="card">
                                        <div class="card-body text-center py-2">
                                            <p class="mb-0">{{ $t('TouchScreen.Received') }}</p>
                                            <h4 class="my-0">{{ sale.salePayment.received }}</h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-6 col-sm-4 col-lg-3 ">
                                    <div class="card">
                                        <div class="card-body text-center py-2">
                                            <p class="mb-0">{{ $t('TouchScreen.Balance') }}</p>
                                            <h4 class="my-0">{{ sale.salePayment.balance }}</h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-6 col-sm-4 col-lg-3 ">
                                    <div class="card">
                                        <div class="card-body text-center py-2">
                                            <p class="mb-0">{{ $t('TouchScreen.Change') }}</p>
                                            <h4 class="my-0">{{ sale.salePayment.change }}</h4>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <h4>{{ $t('TouchScreen.PaymentDetails') }} </h4>
                                    <hr class="hr-dashed hr-menu mt-0" />
                                    <table class="table mb-0">
                                        <thead class="">
                                            <tr v-for="(payment, index) in sale.salePayment.paymentTypes" :key="index + 8">
                                                <th style="width:80%;">
                                                    {{ payment.name }} <span v-if="payment.paymentType == 'OtherCurrency'">
                                                        ({{
                                                        $t('TouchScreen.Amount')
                                                        }}={{ payment.otherAmount }}, {{
        $t('TouchScreen.Rate')
                                                        }}={{ payment.rate }})
                                                    </span> :
                                                    {{ $formatAmount(payment.amount) }}
                                                </th>
                                                <th class="text-center" style="width:20%;">
                                                    <a href="javascript:void(0);" @click="removeFromPaymentList(index)">
                                                        <i class="las la-trash-alt text-secondary font-16"></i>
                                                    </a>
                                                </th>
                                            </tr>
                                        </thead>
                                    </table>
                                </div>
                            </div>

                            <div class="row" style="position: absolute; bottom: 0; width: 100%;">
                                <div class="col-md-6 col-sm-3 d-grid">
                                    <button class="btn btn-primary btn-lg " v-if="isValid('CanPrintTouchInvoice')"
                                            data-bs-dismiss="offcanvas" aria-label="Close" v-on:click="saveSale('Paid', true)"
                                            v-bind:disabled="!isPaymentValid">
                                        {{ $t('TouchScreen.SaveAndPrint') }}
                                    </button>
                                </div>
                                <div class="col-md-3 col-sm-3  d-grid">
                                    <button class="btn btn-primary btn-lg " data-bs-dismiss="offcanvas" aria-label="Close"
                                            v-on:click="saveSale('Paid', false)" v-bind:disabled="!isPaymentValid">
                                        {{ $t('TouchScreen.SaveSale') }}
                                    </button>
                                </div>
                                <div class="col-md-3 col-sm-3  d-grid">
                                    <button class="btn btn-danger btn-lg " style="padding-left:16px;"
                                            v-on:click="Reset(false)" data-bs-dismiss="offcanvas" aria-label="Close">
                                        {{ $t('TouchScreen.Reset') }}
                                    </button>
                                </div>
                                <div class="col-md-12 col-sm-3  d-grid"
                                     v-if="sale.salePayment.dueAmount < 0 && isCashVoucher && cashVoucherAllow == 'true'">
                                    <button class="btn btn-primary" v-if="isValid('CanAddCashVoucher')"
                                            v-on:click="saveSale('Paid', true, 'CashVoucher')" v-bind:disabled="!isPaymentValid">
                                        {{ $t('TouchScreen.GenerateCashVoucher') }}
                                    </button>
                                </div>
                            </div>
                        </div>
                        <div class="col-5" style="position:relative;" v-if="isPay">
                            <div class="row">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-12">
                                            <div class="card mb-0 bg-light-alt" style="line-height: 3.2;">
                                                <div class="table-responsive" style="overflow:hidden;">
                                                    <table class="table mb-0">
                                                        <thead>
                                                            <tr>
                                                                <td style="width:70%;">{{ calculateValueShow }}</td>
                                                                <td class="text-end" style="width:30%;">
                                                                    =
                                                                    {{
 parseFloat(sale.salePayment.received).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                        "$1,")
                                                                    }}
                                                                </td>
                                                            </tr>
                                                        </thead>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row mt-3 displayHidden ">
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-6 d-grid mt-3 "
                                             v-for="item in denominationSetuplist.slice(0, 9)" :key="item.id + 3">
                                            <button class="btn btn-light btn-lg"
                                                    @click="calculatorValuaCalculation(item.number, 'denomination')">
                                                {{ item.number }}
                                            </button>
                                        </div>
                                    </div>

                                    <hr class="hr-dashed hr-menu" />
                                    <div class="row" style="position:absolute; bottom:0; width:100%;">
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-6 d-grid">
                                            <button class="btn btn-light btn-lg mt-1 mb-2"
                                                    @click="calculatorValuaCalculation('7')"
                                                    @shortkey="calculatorValuaCalculation('7')" v-shortkey="['7']">
                                                7
                                            </button>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-6 d-grid">
                                            <button class="btn btn-light btn-lg mt-1 mb-2"
                                                    @click="calculatorValuaCalculation('8')"
                                                    @shortkey="calculatorValuaCalculation('8')" v-shortkey="['8']">
                                                8
                                            </button>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-6 d-grid">
                                            <button class="btn btn-light btn-lg mt-1 mb-2"
                                                    @click="calculatorValuaCalculation('9')"
                                                    @shortkey="calculatorValuaCalculation('9')" v-shortkey="['9']">
                                                9
                                            </button>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-6 d-grid">
                                            <button class="btn btn-light btn-lg mt-1 mb-2"
                                                    @click="calculatorValuaCalculation('4')"
                                                    @shortkey="calculatorValuaCalculation('4')" v-shortkey="['4']">
                                                4
                                            </button>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-6 d-grid">
                                            <button class="btn btn-light btn-lg mt-1 mb-2"
                                                    @click="calculatorValuaCalculation('5')"
                                                    @shortkey="calculatorValuaCalculation('5')" v-shortkey="['5']">
                                                5
                                            </button>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-6 d-grid">
                                            <button class="btn btn-light btn-lg mt-1 mb-2"
                                                    @click="calculatorValuaCalculation('6')"
                                                    @shortkey="calculatorValuaCalculation('6')" v-shortkey="['6']">
                                                6
                                            </button>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-6 d-grid">
                                            <button class="btn btn-light btn-lg mt-1 mb-2"
                                                    @click="calculatorValuaCalculation('1')"
                                                    @shortkey="calculatorValuaCalculation('1')" v-shortkey="['1']">
                                                1
                                            </button>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-6 d-grid">
                                            <button class="btn btn-light btn-lg mt-1 mb-2"
                                                    @click="calculatorValuaCalculation('2')"
                                                    @shortkey="calculatorValuaCalculation('2')" v-shortkey="['2']">
                                                2
                                            </button>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-6 d-grid">
                                            <button class="btn btn-light btn-lg mt-1 mb-2"
                                                    @click="calculatorValuaCalculation('3')"
                                                    @shortkey="calculatorValuaCalculation('3')" v-shortkey="['3']">
                                                3
                                            </button>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-6 d-grid">
                                            <button class="btn btn-light btn-lg mt-1 mb-2"
                                                    @click="calculatorValuaCalculation('c')"
                                                    @shortkey="calculatorValuaCalculation('c')" v-shortkey="['del']">
                                                C
                                            </button>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-6 d-grid">
                                            <button class="btn btn-light btn-lg mt-1 mb-2"
                                                    @click="calculatorValuaCalculation('0')"
                                                    @shortkey="calculatorValuaCalculation('0')" v-shortkey="['0']">
                                                0
                                            </button>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-6 d-grid">
                                            <button class="btn btn-light btn-lg mt-1 mb-2"
                                                    @click="calculatorValuaCalculation('.')"
                                                    @shortkey="calculatorValuaCalculation('.')" v-shortkey="['.']">
                                                .
                                            </button>
                                        </div>
                                        <div class="col-lg-8 col-md-8 col-sm-8 col-8 d-grid">
                                            <button class="btn btn-light btn-lg mt-1"
                                                    @click="calculatorValuaCalculation('back')"
                                                    @shortkey="calculatorValuaCalculation('back')" v-shortkey="['backspace']">
                                                <i class="mdi mdi-close"></i>
                                            </button>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6 col-6 d-grid">
                                            <button @click="addPayment(sale.salePayment.received, sale.salePayment.paymentType, sale.salePayment.paymentName)"
                                                    @shortkey="addPayment(sale.salePayment.received, sale.salePayment.paymentType, sale.salePayment.paymentName)"
                                                    v-shortkey="['enter']"
                                                    v-bind:disabled="!(sale.salePayment.paymentType == 'OtherCurrency' && sale.otherCurrency.currencyId != null && sale.salePayment.received > 0) && !(sale.salePayment.paymentType == 'Bank' && sale.salePayment.paymentOptionId != null && sale.salePayment.received > 0) && !(sale.salePayment.paymentType == 'Cash' && sale.salePayment.received > 0)"
                                                    class="btn btn-light btn-lg mt-1">
                                                {{ $t('TouchScreen.Add') }}
                                            </button>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <invoicedetailsprint :show="showReport" v-if="showReport" :reportsrc="reportsrc" :changereport="changereport"
                                 @close="IsClose" @IsSave="IsSave" />


            <walk-customer-modal :walkcustomer="newsale" :show="walkcustomer" v-if="walkcustomer" @close="WalkIn" />
            <hold-modal :sale="newsale" :show="hold" v-if="hold" @close="holdCustomer" @saveSale="saveSale" />
            <unhold-modal :sale="newsale" :show="unhold" v-if="unhold" @close="UnholdCustomer"
                          @EditTOuchInvocie="EditTOuchInvocie" />

            <return-item-model :postinvoicedetail="postInvoiceDetail" :show="returnItem" v-if="returnItem"
                               @close="returnItemModelClose" @EditTOuchInvocie="EditTOuchInvocie" />
            <cashcounterdetail @close="show1 = false, isDayStart()" :counterCode="counterCode" :cashInHand="cashInHand"
                               :openingCash="openingCash" :bank="bank" :dayStartId="dayStartId" :expense="expense" :counterId="counterId"
                               :show="show1" v-if="show1" :type="type" />

            <quickProductItem :newproduct="newProduct" :show="showProduct" v-if="showProduct"
                              @closeOnSave="onCloseQuickProduct" @close="showProduct = false" :type="type" />
        </div>

        <div v-else class="container" style="background-color: #F4F6FC ;">
            <div class="col-lg-6 col-sm-6 ml-auto mr-auto p-4">
                <div class="card  text-center p-4">
                    <h4 class="">{{ $t('TouchScreen.FirstStartInvoice') }}</h4>
                    <div class="row">
                        <div class="col-md-6">
                            <button class="btn btn-outline-danger btn-block  mr-2" v-on:click="goToSale">
                                <i class="fas fa-arrow-circle-left fa-lg"></i> {{ $t('TouchScreen.Close') }}
                            </button>
                        </div>
                        <div class="col-md-6" v-if="bankDetail">
                            <router-link :to="{ path: '/WholeSaleDay', query: { token_name: 'DayStart_token', fromDashboard: 'true' } }">
                                <a href="javascript:void(0)" class="btn btn-outline-danger btn-block ">
                                    {{
                                        $t('TouchScreen.DayStart')
                                    }}
                                </a>
                            </router-link>
                        </div>
                        <div class="col-md-6" v-else>
                            <router-link :to="{ path: '/dayStart', query: { token_name: 'DayStart_token', fromDashboard: 'true' } }">
                                <a href="javascript:void(0)" class="btn btn-outline-danger btn-block ">
                                    {{
                                        $t('TouchScreen.DayStart')
                                    }}
                                </a>
                            </router-link>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <acessdenied v-else></acessdenied>
</template>

<script scoped>
    import moment from "moment";
    import clickMixin from '@/Mixins/clickMixin'

    import {
        required,
        requiredIf
    } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import 'vue3-carousel/dist/carousel.css';
    import { Carousel, Slide } from 'vue3-carousel';
    export default {
        name: "TouchInvoice",
        mixins: [clickMixin],
        components: {
            Carousel,
            Slide
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                saleDefaultVat: '',
                barCode: '',
                reportsrc: '',
                changereport: 0,

                searchReturnInvoice: '',
                bankDetail: false,
                showReport: false,
                decimalQuantity: false,
                dayStartId: '00000000-0000-0000-0000-000000000000',
                counterId: '00000000-0000-0000-0000-000000000000',
                printTemplate: '',
                printSize: '',
                openingCash: '',
                expense: '',
                cashInHand: '',
                bank: '',
                show1: false,
                searchInvoice: '',
                focusValue: 'calculator',
                TouchInvoice: 'TouchInvoice',
                filePath: null,
                cashVoucherAllow: '',
                isDenomination: false,
                calculateValueShow: '',
                loginUserName: '',
                dayStartTime: '',
                counterCode: '',
                recieve: '',
                denominationSetuplist: [],
                isPay: false,
                isTouchScreen: false,
                companyId: '',
                invoiceWoInventory: '',
                styleclass: '',
                bankList: [],
                currencyList: [],
                autoNumber: '',
                autoNumberCredit: '',
                isAuthour: {
                    changePriceDuringSale: false,
                    giveDiscountDuringSale: false,
                    column: '',
                },
                changePriceDuringSale: false,
                giveDiscountDuringSale: false,
                show: false,
                isCardColor: false,
                walkcustomer: false,
                hold: false,
                unhold: false,
                authorize: {
                    column: '',
                    userName: '',
                    password: '',
                },
                wareHouserender: 0,
                itemRender: 0,
                isSaleReturn: false,
                search: '',
                currentPage: 1,
                pageCount: '',
                rowCount: '',

                categoryId: '00000000-0000-0000-0000-000000000000',
                products: [],
                produc: [],
                saleProducts: [],
                toggleCash: false,
                toggleCredit: true,
                quantityRander: 0,
                rander: 0,
                togglePayCash: 'Cash',
                toggleBank: 'Bank',
                togglePayVoucher: 'CashVoucher',
                toggleOtherCurrency: 'OtherCurrency',
                rendered: 0,
                sale: {
                    id: "00000000-0000-0000-0000-000000000000",
                    date: "",
                    taxMethod: "",
                    barCode: "",
                    terminalId: '00000000-0000-0000-0000-000000000000',
                    dayStart: false,
                    isTouch: true,
                    counterId: '',
                    invoiceWoInventory: "",
                    registrationNo: "",
                    customerId: null,
                    dueDate: "",
                    wareHouseId: "",
                    refrenceNo: '',
                    saleItems: [],
                    isCredit: false,
                    cashCustomer: "Walk-In",
                    documentType: "SaleInvoice",
                    mobile: "",
                    email: "",
                    cashCustomerId: "",
                    code: "",
                    change: 0,
                    invoiceType: "",
                    voucherNo: '',
                    invoicePrefix: '',
                    userName: '',
                    vatAmount: 0,
                    discountAmount: 0,
                    totalAfterDiscount: 0,
                    totalAmount: 0,
                    grossAmount: 0,
                    salePayment: {
                        dueAmount: 0,
                        received: 0,
                        balance: 0,
                        change: 0,
                        paymentTypes: [],
                        paymentType: 'Cash',
                        paymentOptionId: null,
                        paymentName: 'Cash',
                    },
                    otherCurrency: {
                        currencyId: null,
                        rate: 0,
                        amount: 0,
                    },

                    returnInvoiceAmount: 0,
                    isSaleReturnPost: false,
                    branchId: '',
                },
                printId: '00000000-0000-0000-0000-000000000000',
                printDetailsPos: [],
                printDetails: [],
                printDetailsCashVoucher: [],
                categorylist: [],
                printRender: 0,
                quantity: 0,
                loading: false,
                product: {
                    id: "",
                },
                vats: [],
                summary: {
                    item: 0,
                    qty: 0,
                    total: 0,
                    discount: 0,
                    withDisc: 0,
                    vat: 0,
                    withVat: 0,
                    bundleAmount: 0,
                    inclusiveVat: 0
                },
                headerFooter: {
                    returnDays: '',
                    footerEn: '',
                    footerAr: '',
                    printSizeA4: '',
                    company: ''
                },
                CompanyID: '',
                UserID: '',
                employeeId: '',
                isDayAlreadyStart: false,
                IsProduction: false,
                currency: '',
                calculator: false,
                bankId: null,
                otherCurrencyId: null,
                productList: [],
                isInvoiceSearch: false,
                returnItem: false,
                isReturnItem: false,
                postInvoiceDetail: '',
                isCashVoucher: false,
                saleList: [],
                saleId: '00000000-0000-0000-0000-000000000000',
                isDoublePrint: false,
                saleReturnRegNo: '',
                isOldInvoice: false,
                newProduct: {
                    id: '00000000-0000-0000-0000-000000000000',
                    englishName: '',
                    arabicName: '',
                    salePrice: 0,
                    isActive: true,
                    code: '',
                    taxMethod: '',
                    taxRateId: ''

                },
                showProduct: false,
                i: 1,
                overWrite: false,
                BusinessLogo: '',
                BusinessNameArabic: '',
                BusinessNameEnglish: '',
                BusinessTypeArabic: '',
                BusinessTypeEnglish: '',
                CompanyNameArabic: '',
                CompanyNameEnglish: '',
                isFifo: false,
                openBatch: 0,
                taxMethod: '',
                taxRateId: ''
            }
        },
        validations() {
            return {
                saleProducts: {
                    required
                },
                sale: {
                    date: {
                        required
                    },
                    registrationNo: {
                        required
                    },
                    customerId: {},
                    wareHouseId: {
                        required
                    },
                    cashCustomer: {
                        required: requiredIf(function () {
                            return this.sale.customerId == '00000000-0000-0000-0000-000000000000' || this.sale.customerId == null ? true : false;
                        }),

                        //required: requiredIf((x) => {
                        //    if (x.customerId == '00000000-0000-0000-0000-000000000000' ||
                        //        x.customerId == null)
                        //        return true;
                        //    return false;
                        //})
                    },
                }
            }
        },
        computed: {
            isPaymentValid: function () {
                var paymentTypesAmount;
                if (this.sale.salePayment.paymentTypes.length > 0) {
                    paymentTypesAmount = this.sale.salePayment.paymentTypes.reduce((total, payment) =>
                        total + payment.amount, 0);
                } else {
                    paymentTypesAmount = 0;
                }

                if (this.sale.salePayment.paymentType == "Bank") {
                    if (this.sale.salePayment.paymentOptionId == "00000000-0000-0000-0000-000000000000" ||
                        this.sale.salePayment.paymentOptionId == null) {
                        return false;
                    }
                }
                if (this.sale.salePayment.paymentType == "OtherCurrency") {
                    if (this.sale.otherCurrency.currencyId == "00000000-0000-0000-0000-000000000000" ||
                        this.sale.otherCurrency.currencyId == null) {
                        return false;
                    }
                }
                if ((parseFloat(this.sale.salePayment.received) <= parseFloat(this.sale.salePayment.dueAmount))) {
                    if (((parseFloat(this.sale.salePayment.received) + paymentTypesAmount)) >= this.sale.salePayment.dueAmount) {
                        return true
                    }
                } else {
                    if (((parseFloat(this.sale.salePayment.received) + paymentTypesAmount)) >= this.sale.salePayment.dueAmount) {
                        return true
                    }
                }
                return false;
            },
        },
        watch: {
            search: function () {
                this.getProduct(this.categoryId);
            },
            searchReturnInvoice: function (value) {
                this.getInvoiceList(value);
            }
        },
        methods: {

            onCloseQuickProduct: function (product) {
                this.showProduct = false;
                this.addProduct(product[0].id, product[0])
            },
            IsClose: function () {
                this.showReport = false;
            },

            IsSave: function () {
                this.show = !this.show;
            },

            addNewProduct: function () {
                this.newProduct = {
                    id: '00000000-0000-0000-0000-000000000000',
                    englishName: '',
                    arabicName: '',
                    salePrice: 0,
                    isActive: true,
                    code: '',
                    taxMethod: '',
                    unitId: '',
                    levelOneUnit: '',
                    taxRateId: '',
                    barcode: '',
                    categoryId: '',

                }
                this.showProduct = !this.showProduct;
                this.type = "Add";
            },

            DayEnd: function () {
                var root = this;
                var userID = localStorage.getItem('UserID');
                var supervisorLogin = localStorage.getItem('IsSupervisor') == 'true' ? true : false;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Product/IsDayStart?userId=' + userID + '&employeeId' + this.employeeId + '&isSupervisor=' + supervisorLogin, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {
                        if (response.data.isDayStart) {
                            root.dayStartId = response.data.dayStartId;
                            root.isFirstUser = response.data.isFirstUser;
                            root.cashInHand = response.data.cashInHand;
                            root.bank = response.data.bank;
                            root.openingCash = response.data.openingCash;
                            root.show1 = !root.show1;
                            root.type = "Add";

                        } else {
                            console.log(response.date);
                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });
            },

            Reset: function (value) {
                this.sale.cashCustomer = "Walk-In";
                this.isInvoiceSearch = false;
                this.searchInvoice = '';
                this.isTouchScreen = false;
                this.styleclass = '';
                this.currencyList = [];
                this.show = false;
                this.wareHouserender = 0;
                this.itemRender = 0;
                this.search = '';

                this.categoryId = '';
                this.products = [];
                this.saleProducts = [];
                this.toggleCash = false;
                this.toggleCredit = true;
                this.quantityRander = 0;
                this.rander = 0;
                this.togglePayCash = 'Cash';
                this.toggleBank = 'Bank';
                this.togglePayVoucher = 'CashVoucher';
                this.toggleOtherCurrency = 'OtherCurrency';
                this.rendered = 0;

                this.printId = '00000000-0000-0000-0000-000000000000';
                this.printDetailsPos = [];
                this.printDetails = [];
                this.printDetailsCashVoucher = [];
                this.printRender = 0;
                this.quantity = 0;
                this.product = {
                    id: "",
                };
                this.summary = {
                    item: 0,
                    qty: 0,
                    total: 0,
                    discount: 0,
                    withDisc: 0,
                    vat: 0,
                    withVat: 0,
                    bundleAmount: 0,
                    inclusiveVat: 0
                };

                this.bankId = null;
                this.otherCurrencyId = null;
                this.postInvoiceDetail = '';
                this.saleList = [];
                this.saleId = '00000000-0000-0000-0000-000000000000';
                this.sale.id = '00000000-0000-0000-0000-000000000000';
                this.saleReturnRegNo = '';
                this.HidePayment();
                if (value) {
                    this.produc = [];
                    if (localStorage.getItem('OnPageLoadItem') == 'true') {
                        this.getProduct(this.categoryId);
                    }
                    this.AutoIncrementCode();
                }

            },

            ResetProductFilter: function () {
                this.categoryId = ''
                this.search = ''
                this.getProduct(this.categoryId);
            },

            onBarcodeScannedInvoice: function () {
                if (this.i == 6) {
                    console.log(this.searchBarcode + this.i);
                    this.onBarcodeScannedBySearch(this.searchBarcode)
                }
                this.i++
            },

            getDate: function (date) {
                return moment(date).format('LLL');
            },

            FocusInput: function (prop, event) {

                if (prop == 'amount') {
                    this.focusValue = prop;
                    console.log(event.target.select());
                }
                if (prop == 'rate') {
                    this.focusValue = prop;
                    console.log(event.target.select());
                }
                if (prop == 'voucher') {
                    this.focusValue = prop;
                    console.log(event.target.select());
                }
                this.calculator = true;
            },

            FocusOutInput: function () {
                //
                //this.focusValue = 'calculator';
                console.log(this.focusValue);
            },

            OtherCurrencyCalculate: function () {
                this.sale.salePayment.received = parseFloat(this.sale.otherCurrency.amount == '' ? 0 : this.sale.otherCurrency.amount).toFixed(3).slice(0, -1) * parseFloat(this.sale.otherCurrency.rate == '' ? 0 : this.sale.otherCurrency.rate).toFixed(3).slice(0, -1);
            },

            VerifyCashVoucher: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Sale/VerifyCashVouch?voucherNo=' + this.sale.voucherNo, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {
                        if (response.data != null) {

                            var voucherDetail = response.data;
                            if (root.sale.salePayment.paymentTypes.filter(x => x.paymentType == 'CashVoucher').length == 0) {
                                root.sale.salePayment.paymentOptionId = response.data.id;
                                root.addPayment(voucherDetail.amount, 'CashVoucher', 'CashVoucher');

                            }
                        }
                    }).catch(error => {

                        root.$swal.fire({
                            icon: 'error',
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: error.response.data,
                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });

                    });
            },

            getInvoiceList: function (search) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var branchId = localStorage.getItem('BranchId');

                this.$https.get('/Sale/SaleList?searchTerm=' + search + '&isDropdown=true' + '&status=' + 'Paid' + '&branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.saleList = response.data.results.sales;
                        }
                    }).catch(error => {
                        root.$swal.fire({
                            icon: 'error',
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: error.response.data,
                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });
                    });
            },

            getPostInvoiceDetail: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https.get('/Sale/SaleDetail?id=' + id, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {
                        if (response.data != null) {
                            root.postInvoiceDetail = response.data;
                            root.postInvoiceDetail.date = moment(root.postInvoiceDetail.date).format("LLL");
                            root.isCashVoucher = true;
                            root.openReturnItemModel();
                        } else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: response.data,
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                icon: 'error',
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    }).catch(error => {
                        root.$swal.fire({
                            icon: 'error',
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: error.response.data,
                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });
                    });

            },

            serachInvoiceByBarcode: function (value) {

                this.isInvoiceSearch = value;
                if (!value) {
                    this.search = '';
                } else {
                    this.postInvoiceDetail = '';
                    this.saleList = [];
                }
            },

            GetProductList: function () {

                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }

                this.isRaw = this.raw == undefined ? false : this.raw;
                var warehouseId = ''
                if (this.invoiceWoInventory == 'false')
                    warehouseId = this.sale.wareHouseId

                this.$https
                    .get("/Product/GetProductBarcode?isRaw=" + root.isRaw + '&wareHouseId=' + warehouseId, {
                        headers: {
                            Authorization: `Bearer ${token}`
                        },
                    })
                    .then(function (response) {

                        if (response.data != null) {
                            root.productList = response.data.results.products;

                        }
                    });

            },

            onBarcodeScannedBySearch(barcode) {

                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }

                var warehouseId = ''
                if (this.invoiceWoInventory == 'false')
                    warehouseId = this.sale.wareHouseId

                if (this.isInvoiceSearch == true) {
                    this.getInvoiceList(barcode);
                    root.searchBarcode = ''
                    root.i = 0
                } else {

                    this.$https.get('/Product/GetProductInformationForTouch?categoryId=' + this.categoryId + '&searchTerm=' + barcode + '&wareHouseId=' + warehouseId, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    })

                        .then(function (response) {

                            if (response.data != null) {
                                console.log("msg is:" + root.searchBarcode)
                                var product = response.data.results.products.find(x => x.barCode == root.searchBarcode)
                                console.log(product.englishName)
                                var product1 = {};
                                product1.id = product.id;
                                product1.englishName = product.englishName != '' ? product.englishName : product.arabicName;
                                product1.arabicName = product.arabicName != '' ? product.arabicName : product.englishName;
                                product1.assortment = product.assortment;
                                product1.barCode = product.barCode;
                                product1.basicUnit = product.basicUnit;
                                product1.brandId = product.brandId;
                                product1.bundleCategory = product.bundleCategory;
                                product1.categoryId = product.categoryId;
                                product1.code = product.code;
                                product1.colorId = product.colorId;
                                product1.colorName = product.colorName;
                                product1.colorNameArabic = product.colorNameArabic;
                                product1.description = product.description;
                                product1.inventory = product.inventory;
                                product1.isActive = product.isActive;
                                product1.image = product.image;
                                product1.isExpire = product.isExpire;
                                product1.isRaw = product.isRaw;
                                product1.length = product.length;
                                product1.levelOneUnit = product.levelOneUnit;
                                product1.stockLevel = product.stockLevel;
                                product1.originId = product.originId;
                                product1.promotionOffer = product.promotionOffer;
                                product1.purchasePrice = product.purchasePrice;
                                product1.salePrice = product.salePrice;
                                product1.salePriceUnit = product.salePriceUnit;
                                product1.saleReturnDays = product.saleReturnDays;
                                product1.shelf = product.shelf;
                                product1.sizeId = product.sizeId;
                                product1.sizeName = product.sizeName;
                                product1.sizeNameArabic = product.sizeNameArabic;
                                product1.styleNumber = product.styleNumber;
                                product1.subCategoryId = product.subCategoryId;
                                product1.taxMethod = product.taxMethod;
                                product1.taxRate = product.taxRate;
                                product1.taxRateId = product.taxRateId;
                                product1.unit = product.unit;
                                product1.unitId = product.unitId;
                                product1.unitPerPack = product.unitPerPack;
                                product1.width = product.width;

                                if (root.saleProducts.length > 0) {
                                    var existingProduct = root.saleProducts.find(x => x.productId == product1.id)
                                    if (existingProduct != null) {
                                        root.quantityIncrease(product1.id)
                                    } else {
                                        root.addProduct(product1.id, product1)
                                    }
                                } else {
                                    root.addProduct(product1.id, product1)
                                }
                                root.addProduct(product1.id, product1)

                            } else {
                                root.$swal.fire({
                                    icon: 'error',
                                    title: 'Item not found',
                                    text: "Item not found in this scanned barcode",
                                    showConfirmButton: false,
                                    timer: 5000,
                                    timerProgressBar: true,
                                });
                            }
                            root.searchBarcode = ''
                            root.i = 0
                        }).catch(error => {
                            console.log(error)
                            root.$swal.fire({
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                            root.loading = false
                            root.searchBarcode = ''
                            root.i = 0
                        })
                        .finally(() => {
                            root.loading = false;
                            root.searchBarcode = ''
                            root.i = 0
                        })

                }

            },

            onBarcodeScanned(barcode) {
                
                if (localStorage.getItem("BarcodeScan") != 'TouchScreen')
                    return
                var root = this;
                var token = localStorage.getItem("token");
                var warehouseId = ''
                if (this.invoiceWoInventory == 'false')
                    warehouseId = this.sale.wareHouseId

                if (this.isInvoiceSearch == true) {
                    this.getInvoiceList(barcode);
                }
                else {

                    this.$https.get('/Product/GetProductDetailForInvoiceQuery?barCode=' + barcode + '&wareHouseId=' + warehouseId + "&isDropdown=true" + '&categoryId=' + this.categoryId, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            if (response.data != null) {
                                var newProduct = response.data;
                                root.barCode = '';

                                if (root.saleProducts.length > 0) {
                                    var existingProduct = root.saleProducts.find(x => x.productId == newProduct.id)
                                    if (existingProduct != null) {
                                        root.quantityIncrease(newProduct.id)
                                    }
                                    else {
                                        root.addProduct(newProduct.id, newProduct)
                                    }
                                }
                                else {
                                    root.addProduct(newProduct.id, newProduct)
                                }

                            }
                            else {
                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                    text: "Item not found",
                                    type: 'error',
                                    confirmButtonClass: "btn btn-danger",
                                    icon: 'error',
                                    timer: 1000,
                                    timerProgressBar: true,
                                });
                            }
                        });

                }

            },

            GetDenominationSetupData: function () {
                var root = this;
                var url = '/Product/DenominationSetupList?isActive=true';
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get(url, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {
                    if (response.data != null) {
                        root.denominationSetuplist = response.data.denominationSetups;

                    }
                });
            },

            calculatorValuaCalculation: function (value, type) {


                var check = false;


                if (this.focusValue == 'amount' && value != 'c') {

                    if (value == 'back') {
                        if (this.sale.otherCurrency.amount != '' && this.sale.otherCurrency.amount != 0) {
                            this.sale.otherCurrency.amount = this.sale.otherCurrency.amount.slice(0, -1);
                        }
                    }
                    else {
                        if (this.sale.otherCurrency.amount == 0) {
                            this.sale.otherCurrency.amount = value;
                        }
                        else {
                            this.sale.otherCurrency.amount = this.sale.otherCurrency.amount + value;
                        }
                    }
                    this.OtherCurrencyCalculate();
                }
                else if (this.focusValue == 'rate' && value != 'c') {
                    if (value == 'back') {
                        if (this.sale.otherCurrency.rate != '' && this.sale.otherCurrency.rate != 0) {
                            this.sale.otherCurrency.rate = this.sale.otherCurrency.rate.slice(0, -1);
                        }
                    }
                    else {
                        if (this.sale.otherCurrency.rate == 0) {
                            this.sale.otherCurrency.rate = value;
                        }
                        else {
                            this.sale.otherCurrency.rate = this.sale.otherCurrency.rate + value;
                        }
                    }
                    this.OtherCurrencyCalculate();
                }
                else if (this.focusValue == 'voucher' && value != 'c') {
                    if (value == 'back') {
                        if (this.sale.voucherNo != '') {
                            this.sale.voucherNo = this.sale.voucherNo.slice(0, -1);
                        }
                    }
                    else {
                        this.sale.voucherNo = this.sale.voucherNo + value;
                    }
                }
                else {
                    var total = 0;
                    if (type == 'denomination') {

                        this.isDenomination = true;
                        if (this.recieve == '' || this.recieve == 0) {
                            this.calculateValueShow = value.toString()
                        } else {
                            if (this.calculateValueShow[this.calculateValueShow.length - 1] == '+') {
                                this.calculateValueShow = this.calculateValueShow + value;
                            } else {
                                this.calculateValueShow = this.calculateValueShow + '+' + value;
                            }
                        }

                        if (this.recieve == '') {
                            this.recieve == 0;
                            this.recieve = this.recieve + value;
                        } else {
                            this.recieve = (parseFloat(this.recieve) + value).toString();
                        }
                        const plus = "+";
                        check = this.calculateValueShow.includes(plus);
                        if (check) {
                            this.myResult = this.calculateValueShow.split("+");
                            total = this.myResult.reduce((total, prod) => total + parseFloat(prod), 0);

                            this.sale.salePayment.received = total;
                        } else {

                            /*focus in otherCurrency input */
                            this.sale.salePayment.received = this.recieve;
                        }
                    } else {
                        if (value == 'c') {
                            this.recieve = '';
                            this.sale.salePayment.received = 0;
                            this.sale.otherCurrency.amount = 0;
                            this.sale.otherCurrency.rate = 0;
                            this.sale.voucherNo = '';
                            this.focusValue == 'calculator'
                            this.calculateValueShow = '';
                            this.isDenomination = false;
                        } else if (value == 'back') {

                            this.calculateValueShow = this.calculateValueShow.slice(0, -1);
                            if (this.calculateValueShow == "") {
                                this.recieve = '';
                                this.sale.salePayment.received = 0;
                                this.isDenomination = false;
                            } else {
                                var plus = "+";
                                check = this.calculateValueShow.includes(plus);
                                if (check) {
                                    var lastDig = this.calculateValueShow[this.calculateValueShow.length - 1];
                                    console.log(lastDig);
                                    if (lastDig == '+') {
                                        var calculateValueShowNew = this.calculateValueShow.slice(0, -1);
                                        this.myResult = calculateValueShowNew.split("+");
                                        total = this.myResult.reduce((total, prod) =>
                                            total + parseFloat(prod), 0);

                                        this.sale.salePayment.received = total;
                                    } else {
                                        this.myResult = this.calculateValueShow.split("+");
                                        total = this.myResult.reduce((total, prod) =>
                                            total + parseFloat(prod), 0);

                                        this.sale.salePayment.received = total;
                                    }

                                } else {
                                    this.sale.salePayment.received = this.calculateValueShow;

                                    this.recieve = this.calculateValueShow;
                                }
                            }
                        } else {

                            if (this.recieve == '') {
                                this.recieve = this.recieve + value;
                                if (this.calculateValueShow[this.calculateValueShow.length - 1] == '+') {
                                    this.calculateValueShow = this.isDenomination ? this.calculateValueShow + (value == '.' ? '0.' : value) : this.calculateValueShow + ((this.recieve == '.' && value == '.') ? '0.' : value);

                                } else {
                                    this.calculateValueShow = this.isDenomination ? this.calculateValueShow + '+' + (value == '.' ? '0.' : value) : this.calculateValueShow + ((this.recieve == '.' && value == '.') ? '0.' : value);

                                }
                                const plus1 = "+";
                                check = this.calculateValueShow.includes(plus1);
                                if (check) {
                                    this.myResult = this.calculateValueShow.split("+");
                                    total = this.myResult.reduce((total, prod) =>
                                        total + parseFloat(prod), 0);

                                    this.sale.salePayment.received = total;
                                } else {

                                    this.sale.salePayment.received = this.recieve;
                                }
                                this.isDenomination = false;
                            } else {
                                var lastDigit = this.recieve.slice(-1);

                                if (lastDigit == '.' && value == '.') {
                                    console.log(lastDigit);
                                } else {

                                    var lastResult = this.calculateValueShow.split("+");
                                    var val = lastResult[lastResult.length - 1];
                                    const dot = ".";
                                    var checkDot = val.includes(dot);

                                    if (checkDot && value == '.') {
                                        console.log('Cannot Repeat Dot');
                                    } else {
                                        this.recieve = this.recieve + value;
                                        if (this.calculateValueShow[this.calculateValueShow.length - 1] == '+') {
                                            this.calculateValueShow = this.isDenomination ? this.calculateValueShow + (value == '.' ? '0.' : value) : this.calculateValueShow + ((this.recieve == '.' && value == '.') ? '0.' : value);
                                        } else {
                                            this.calculateValueShow = this.isDenomination ? this.calculateValueShow + '+' + (value == '.' ? '0.' : value) : this.calculateValueShow + ((this.recieve == '.' && value == '.') ? '0.' : value);
                                        }
                                        const plus1 = "+";
                                        check = this.calculateValueShow.includes(plus1);
                                        if (check) {
                                            this.myResult = this.calculateValueShow.split("+");
                                            total = this.myResult.reduce((total, prod) =>
                                                total + parseFloat(prod), 0);

                                            this.sale.salePayment.received = total;
                                        } else {

                                            this.sale.salePayment.received = this.recieve;
                                        }
                                        this.isDenomination = false;
                                    }
                                }
                            }

                        }
                    }
                }

            },

            calculatorPanelOpen: function (value) {
                this.calculator = value;
                this.focusValue = 'calculator';
            },

            select: function () {
                this.$refs.input.select();
            },


            EditTOuchInvocie: function (data) {


                var root = this;

                this.saleReturnRegNo = data.registrationNo;
                this.sale.isSaleReturnPost = true;
                this.isOldInvoice = true;
                this.saleProducts = []; {
                    if (data.isReturnItem) {
                        this.sale.id = '00000000-0000-0000-0000-000000000000';
                    } else {
                        this.sale.id = data.id;
                        this.sale.registrationNo = data.registrationNo;
                    }
                    this.sale.invoiceType = data.invoiceType;
                    this.sale.cashCustomer = data.cashCustomer;
                    this.sale.dueDate = moment(data.dueDate).format("LLL");
                    this.sale.refrenceNo = data.refrenceNo;
                    this.sale.mobile = data.mobile;
                    this.sale.code = data.code;
                    this.sale.customerId = data.customerId;
                    this.sale.saleItems = data.saleItems;
                    this.sale.wareHouseId = data.wareHouseId;
                    this.sale.cashCustomerId = data.cashCustomerId;
                    this.sale.email = data.email;
                    this.sale.saleInvoiceId = data.id;
                    this.sale.isReturnItem = data.isReturnItem;
                    this.sale.salePayment = {
                        dueAmount: 0,
                        received: 0,
                        balance: 0,
                        change: 0,
                        paymentTypes: [],
                        paymentType: 'Cash',
                        paymentOptionId: '',
                        paymentName: 'Cash',
                    };
                }
                this.getData();
                if (this.$route.query.mobiledata != undefined) {
                    for (var j = 0; j < this.$route.query.mobiledata.mobileOrderItemLookupModels.length; j++) {
                        this.saleProducts.quantity[j] = this.$route.query.mobiledata.mobileOrderItemLookupModels[j].quantity[j];
                    }
                    root.calcuateSummary();
                    this.saleProducts.rowId = this.$route.query.mobiledata.mobileOrderItemLookupModels.rowId;
                    this.saleProducts.quantity = this.$route.query.mobiledata.mobileOrderItemLookupModels.quantity;
                }

            },

            quantityIncrease: function (productId) {

                if (this.saleProducts.some(x => x.productId == productId)) {
                    var prd = this.saleProducts.find(x => x.productId == productId);
                    if (prd.quantity < 0) {
                        prd.quantity--;
                    } else {
                        prd.quantity++;
                    }
                    this.updateLineTotal(prd.quantity, "quantity", prd);
                }
            },

            quantityDecrease: function (productId) {

                if (this.saleProducts.some(x => x.productId == productId)) {
                    var prd = this.saleProducts.find(x => x.productId == productId);
                    if (prd.quantity < 0) {
                        prd.quantity++;
                    } else {
                        prd.quantity--;
                    }
                    this.updateLineTotal(prd.quantity, "quantity", prd);
                }
            },

            WalkIn: function (x) {

                if (x == false) {
                    this.show = false;
                    this.walkcustomer = false;
                } else {
                    this.sale.cashCustomer = x.cashCustomer;
                    this.sale.mobile = x.mobile;
                    this.sale.customerId = x.customerId;
                    this.show = false;
                    this.walkcustomer = false;
                }
            },

            openWalkModel: function () {
                this.newsale = this.sale;

                this.walkcustomer = !this.walkcustomer;
            },

            holdCustomer: function (x) {

                if (x == false) {
                    this.show = false;
                    this.hold = false;
                } else {
                    this.sale.refrenceNo = x.refrenceNo;
                    this.show = false;
                    this.hold = false;
                }
            },

            openHoldModel: function () {
                this.newsale = {
                    id: "00000000-0000-0000-0000-000000000000",
                    refrenceNo: this.sale.refrenceNo,
                    invoiceType: 'Hold',

                },
                    this.hold = !this.hold;
            },

            UnholdCustomer: function (x) {

                if (x == false) {
                    this.show = false;
                    this.unhold = false;
                } else {
                    this.sale.refrenceNo = x.refrenceNo;
                    this.show = false;
                    this.unhold = false;
                }
            },

            returnItemModelClose: function (x) {

                if (x == false) {
                    this.show = false;
                    this.returnItem = false;
                } else {
                    this.show = false;
                    this.returnItem = false;
                }
            },

            openUnHoldModel: function () {
                this.newsale = {
                    id: "00000000-0000-0000-0000-000000000000",
                }
                this.unhold = !this.unhold;
            },

            openReturnItemModel: function () {
                if (this.headerFooter.returnDays == 'null') {
                    if (this.postInvoiceDetail != '' && this.postInvoiceDetail != undefined && this.postInvoiceDetail != null) {
                        this.returnItem = !this.returnItem;
                    }
                } else {
                    var invoiceDate = moment(this.postInvoiceDetail.date).add(this.headerFooter.returnDays, 'days').format("LLL");
                    var todayDate = moment().format("LLL");
                    var isTrue = moment(invoiceDate).isSameOrAfter(todayDate);
                    if (isTrue) {
                        if (this.postInvoiceDetail != '' && this.postInvoiceDetail != undefined && this.postInvoiceDetail != null) {
                            this.returnItem = !this.returnItem;
                        }
                    } else {
                        this.$swal({
                            title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: this.postInvoiceDetail.registrationNo + ' is Expired',
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 1500,
                            timerProgressBar: true,
                        });
                    }
                }
            },

            GetCategoryData: function (search, isActive) {


                var root = this;

                var url = '/Product/GetCategoryInformation?isActive=' + isActive + '&isTouch=true' + '&searchTerm=' + search;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {

                        root.categorylist = [];
                        response.data.results.categories.forEach(function (cat) {
                            root.categorylist.push({
                                id: cat.id,
                                name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (cat.name != '' ? cat.name : cat.nameArabic) : (cat.nameArabic != '' ? cat.nameArabic : cat.name)
                            })
                        })
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                    }
                });

            },

            result: function (x) {
                this.isAuthour = x;
            },

            openmodel: function (column) {

                this.authorize = {
                    userName: '',
                    password: '',
                    column: column
                }
                this.show = !this.show;
            },

            openmodel1: function (column) {

                this.authorize = {
                    userName: '',
                    password: '',
                    column: column
                }
                this.show = !this.show;
            },

            openmodel2: function (column) {

                this.authorize = {
                    userName: '',
                    password: '',
                    column: column
                }
                this.show = !this.show;
            },

            getProduct: function (categoryId) {
                this.categoryId = categoryId;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.produc = [];
                
                if (this.invoiceWoInventory == 'false') {
                    this.$https.get('/Product/GetProductListForDropdown?categoryId=' + this.categoryId + '&searchTerm=' + this.search + '&branchId=' + localStorage.getItem('BranchId'), {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    })
                        .then(function (response) {
                            if (response.data != null) {

                                response.data.results.products.forEach(function (product) {
                                    root.produc.push({
                                        id: product.id,
                                        displayName: product.displayName,
                                        barCode: product.barCode,
                                        categoryId: product.categoryId,
                                        code: product.code,
                                        description: product.description,
                                        salePrice: product.salePrice,
                                        taxMethod: product.taxMethod,
                                        taxRate: product.taxRate,
                                        taxRateId: product.taxRateId,
                                    })
                                })
                                root.pageCount = response.data.pageCount;
                                root.rowCount = response.data.rowCount;
                            }
                        }).catch(error => {
                            root.$swal.fire({
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: error.response.data,
                                showConfirmButton: true,

                            });

                        });
                }
                else {
                    this.$https.get('/Product/GetProductInformation?categoryId=' + this.categoryId + '&searchTerm=' + this.search + '&isDropdown=' + true + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch + '&wareHouseId=' + '', {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    }).then(function (response) {
                        if (response.data != null) {
                            root.produc = response.data.results.products;
                            root.pageCount = response.data.pageCount;
                            root.rowCount = response.data.rowCount;
                        }
                    })
                }
            },

            getBank: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https.get('/Product/PaymentOptionsList?isActive=true', {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {
                    if (response.data != null) {
                        root.bankList = response.data.paymentOptions;
                    }
                })
            },

            getCurrency: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https.get('/Product/CurrencyList', {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {
                    if (response.data != null) {
                        root.currencyList = response.data.currencies;
                        root.currencyList.splice(0, 1);
                    }
                })
            },

            changeProduct: function (NewProdId, rowId) {
                this.saleProducts = this.saleProducts.filter(x => x.rowId != rowId);
                this.addProduct(NewProdId);

            },

            getVatValue: function (id, prod) {
                var vat = this.vats.find((value) => value.id == id);
                prod.taxRateId = id;
                prod.rate = vat.rate;
                this.updateLineTotal(prod.unitPrice, "unitPrice", prod);
                return vat.rate;
            },

            removeProduct: function (id) {
                this.saleProducts = this.saleProducts.filter((prod) => {
                    return prod.rowId != id;
                });

                this.calcuateSummary();
            },

            getData: function () {
                var root = this;




                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                root.$https
                    .get("/Product/TaxRateList", {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.vats = response.data.taxRates;
                        }
                    }).then(function () {

                        if (root.sale.saleItems != undefined) {

                            root.sale.saleItems.forEach(function (item) {
                                var vat = root.vats.find((value) => value.id == item.taxRateId);
                                if (root.sale.isReturnItem) {
                                    //if (item.returnQuantity != 0 && item.returnQuantity != '' && item.returnQuantity != undefined && item.returnQuantity <= item.quantity)
                                    //{
                                    root.products.push(item.product);

                                    root.saleProducts.push({
                                        rowId: item.id,
                                        productName: item.productName,
                                        code: item.code,
                                        productId: item.productId,
                                        unitPrice: item.unitPrice == 0 ? '0' : item.unitPrice,
                                        quantity: parseFloat(item.returnQuantity),
                                        returnQuantity: item.returnQuantity == '' || item.returnQuantity == undefined ? 0 : parseFloat(item.returnQuantity),
                                        discount: item.discount,
                                        fixDiscount: item.fixDiscount,
                                        taxRateId: item.taxRateId,
                                        taxMethod: item.taxMethod,
                                        offerQuantity: item.offerQuantity,
                                        rate: vat.rate,
                                        saleReturnDays: item.saleReturnDays,
                                        lineTotal: item.unitPrice * item.quantity,
                                        vatAmount: 0,
                                        discountAmount: 0,
                                        totalAmount: 0,
                                        grossAmount: 0,
                                    });

                                    var product1 = root.saleProducts.find((x) => {
                                        return x.productId == item.productId;
                                    });
                                    root.getVatValue(product1.taxRateId, product1);
                                    root.updateLineTotal(item.returnQuantity, "quantity", product1);
                                    root.product.id = "";
                                    root.rendered++;
                                    /*}*/

                                } else {
                                    root.products.push(item.product);

                                    root.saleProducts.push({
                                        rowId: item.id,
                                        productName: item.productName,
                                        code: item.code,
                                        productId: item.productId,
                                        unitPrice: item.unitPrice == 0 ? '0' : item.unitPrice,
                                        quantity: item.quantity,
                                        returnQuantity: item.returnQuantity == '' || item.returnQuantity == undefined ? 0 : item.returnQuantity,
                                        discount: item.discount,
                                        fixDiscount: item.fixDiscount,
                                        taxRateId: item.taxRateId,
                                        taxMethod: item.taxMethod,
                                        rate: vat.rate,
                                        saleReturnDays: item.saleReturnDays,
                                        lineTotal: item.unitPrice * item.quantity,
                                    });

                                    var product = root.saleProducts.find((x) => {
                                        return x.productId == item.productId;
                                    });
                                    root.getVatValue(product.taxRateId, product);
                                    root.updateLineTotal(item.quantity, "quantity", product);
                                    root.product.id = "";
                                    root.rendered++;
                                }

                            });
                            root.$emit("details", root.saleProducts);
                        }
                        root.itemRender++;
                    });
            },

            selectCurrency: function (id, name) {
                this.bankId = null;
                this.otherCurrencyId = id;
                this.isActive = true;
                this.sale.salePayment.paymentName = name;
                this.sale.otherCurrency.currencyId = id;
            },

            selectBank: function (id, name) {
                this.otherCurrencyId = null;
                this.bankId = id;
                this.isActive = true;
                this.sale.salePayment.paymentOptionId = id;
                this.sale.salePayment.paymentName = name;
            },

            isPayment: function (credit) {

                this.otherCurrencyId = null;
                this.bankId = null;
                if (credit == 'Cash') {
                    this.sale.salePayment.paymentName = 'Cash';
                }
                this.sale.salePayment.paymentType = credit;
                this.focusValue = 'calculator';
            },
            isCredit: function (credit) {

                this.sale.isCredit = credit;
                if (!this.sale.isCredit) {
                    this.sale.customerId = null;
                }

            },

            emptyCashCustomer: function (customerId) {
                this.sale.customerId = customerId;
                if (this.sale.customerId != '00000000-0000-0000-0000-000000000000' && this.sale.customerId != null) {
                    this.sale.cashCustomer = "Walk-In";
                    this.sale.mobile = "";
                }
            },

            setCustomer: function (value) {
                console.log(value);
            },

            calculatBalance: function (received) {

                if (received == '') {
                    received = 0;
                }
                var paymentTypesAmount;
                if (this.sale.salePayment.paymentTypes.length > 0) {
                    paymentTypesAmount = this.sale.salePayment.paymentTypes.reduce((total, payment) =>
                        total + payment.amount, 0);
                } else {
                    paymentTypesAmount = 0;
                }

                this.sale.salePayment.balance = ((parseFloat(received) + paymentTypesAmount) - this.sale.salePayment.dueAmount <= 0 ? (parseFloat(received) + paymentTypesAmount) - this.sale.salePayment.dueAmount : 0).toFixed(3);
                this.sale.salePayment.change = ((parseFloat(received) + paymentTypesAmount) - this.sale.salePayment.dueAmount > 0 ? (parseFloat(received) + paymentTypesAmount) - this.sale.salePayment.dueAmount : 0).toFixed(3);
                this.sale.change = this.sale.salePayment.change;
            },

            addPayment: function (amount, paymentType, name) {
                var root = this;
                this.sale.salePayment.received = 0;
                this.calculateValueShow = '';
                this.isDenomination = false;
                this.recieve = '';

                if (paymentType == 'Cash') {
                    var payment = this.sale.salePayment.paymentTypes.find((x) => x.paymentType == paymentType)
                    if (payment != undefined) {
                        payment.amount += parseFloat(amount);
                        payment.name = name;
                    } else {
                        this.sale.salePayment.paymentTypes.push({
                            paymentType: paymentType,
                            amount: parseFloat(amount),
                            name: name,
                            rate: 0,
                            otherAmount: 0
                        });
                    }
                } else if (paymentType == 'Bank') {
                    this.sale.salePayment.paymentTypes.push({
                        paymentType: paymentType,
                        amount: parseFloat(amount),
                        name: name,
                        rate: 0,
                        otherAmount: 0
                    });
                } else if (paymentType == 'CashVoucher') {
                    this.sale.salePayment.paymentTypes.push({
                        paymentType: paymentType,
                        amount: parseFloat(amount),
                        name: name,
                        rate: 0,
                        otherAmount: 0
                    });
                } else {
                    this.sale.salePayment.paymentTypes.push({
                        paymentType: paymentType,
                        amount: parseFloat(amount),
                        name: name,
                        rate: root.sale.otherCurrency.rate,
                        otherAmount: root.sale.otherCurrency.amount
                    });
                }

                this.calculatBalance(0);
                this.sale.otherCurrency.amount = 0;
                this.sale.otherCurrency.rate = 0;

            },

            removeFromPaymentList: function (paymentType) {

                this.sale.salePayment.paymentTypes.splice(paymentType, 1);

                this.calculatBalance(0);
            },
            HidePayment: function () {

                this.isPay = false;
                this.calculator = false;
            },
            updateSummary: function () {

                this.isPay = true;
                this.sale.salePayment.dueAmount = parseFloat(this.summary.withVat);
                this.sale.salePayment.received = 0;
                this.sale.salePayment.balance = 0;
                this.sale.salePayment.change = 0;
                this.sale.salePayment.paymentTypes = [];
                this.sale.otherCurrency.amount = 0;
                this.sale.otherCurrency.rate = 0;
                this.sale.salePayment.paymentType = 'Cash'

                var root = this
                setTimeout(function () {
                    root.calculatorPanelOpen(true);
                }, 375)

            },
            AutoIncrementCode: function () {
                var root = this;

                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                root.$https
                    .get("/Sale/SaleAutoGenerateNo?userId=" + localStorage.getItem('UserID') + '&terminalId=' + localStorage.getItem('TerminalId') + '&invoicePrefix=' + localStorage.getItem('InvoicePrefix') + '&branchId=' + localStorage.getItem('BranchId'), {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            if (root.sale.id == '00000000-0000-0000-0000-000000000000') {
                                root.sale.registrationNo = response.data.paid;
                                root.autoNumber = response.data.hold;
                                root.autoNumberCredit = response.data.credit;
                            }
                            else {
                                root.autoNumber = response.data.paid;
                            }
                        }
                    });
            },
            SaveSaleItems: function (saleItems) {

                this.sale.saleItems = saleItems;
            },
            saveSale: function (sale, isPrint, cashVoucher) {

                this.loading = true;
                var root = this;
                if (sale.invoiceType == "Hold") {
                    root.sale.invoiceType = sale.invoiceType;
                    root.sale.refrenceNo = sale.refrenceNo;
                }
                else {
                    root.sale.invoiceType = sale;
                }
                console.log(root.sale);

                if (cashVoucher == 'CashVoucher') {
                    this.sale.isCashVoucher = true;
                    root.headerFooter.printSizeA4 = 'CashVoucher'
                }

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                if (this.sale.salePayment.received != 0) {
                    this.addPayment(this.sale.salePayment.received, this.sale.salePayment.paymentType, this.sale.salePayment.paymentName)
                }

                if (!this.sale.isCredit) {
                    this.sale.dueDate = moment().format("LLL");
                }
                if (this.sale.salePayment.dueAmount < 0) {
                    this.sale.salePayment.received = this.sale.salePayment.dueAmount;
                }
                this.sale.saleItems = this.saleProducts;
                this.sale.taxMethod = localStorage.getItem('taxMethod');

                this.sale.userName = localStorage.getItem('LoginUserName');
                this.sale.terminalId = localStorage.getItem('TerminalId');
                this.sale.invoicePrefix = localStorage.getItem('InvoicePrefix');
                this.sale.branchId = localStorage.getItem('BranchId');


                this.$https
                    .post('/Sale/SaveSaleInformation', root.sale, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {

                        if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Add") {
                            root.loading = false;
                            root.isPay = false;

                            if (root.sale.invoiceType == 'Paid') {
                                if (isPrint) {

                                    root.printId = response.data.message.id;
                                    if (root.headerFooter.printSizeA4 == 'Thermal') {

                                        root.isDoublePrint = response.data.message.isDoublePrint
                                        root.saleId = response.data.message.saleId
                                        root.PrintCashVoucher(root.printId);
                                    }
                                    else if (root.headerFooter.printSizeA4 == 'CashVoucher') {


                                        root.PrintCashVoucher(root.printId);
                                    } else {

                                        root.PrintCashVoucher(root.printId);
                                    }

                                    var paid = '';
                                    if (this.$ServerIp.includes('localhost')) {

                                        var total1 = root.sale.salePayment.paymentTypes.reduce((total, prod) =>
                                            total + prod.amount, 0).toFixed(3).slice(0, -1);
                                        if (parseFloat(root.summary.withVat).toFixed(3).slice(0, -1) < 0) {
                                            paid = 'Payable Amount : ' + parseFloat(root.summary.withVat).toFixed(3).slice(0, -1);
                                        } else {
                                            paid = 'Total   : ' + parseFloat(root.summary.withVat).toFixed(3).slice(0, -1) + '<br>' +
                                                'Paid    : ' + total1 + '<br>' +
                                                'Balance : ' + root.sale.salePayment.change;
                                        }

                                        root.$swal({
                                            html: '<pre><h5>' + paid + '</h5></pre>',
                                            type: 'success',
                                            confirmButtonClass: "btn btn-primary",
                                            buttonStyling: false
                                        }).then(function (ok) {
                                            if (ok != null) {
                                                root.Reset(true);
                                            }
                                        });
                                    }
                                }
                                else {

                                    var total = root.sale.salePayment.paymentTypes.reduce((total, prod) =>
                                        total + prod.amount, 0).toFixed(3).slice(0, -1);

                                    if (parseFloat(root.summary.withVat).toFixed(3).slice(0, -1) < 0) {
                                        paid = 'Payable Amount : ' + parseFloat(root.summary.withVat).toFixed(3).slice(0, -1);
                                    } else {
                                        paid = 'Total   : ' + parseFloat(root.summary.withVat).toFixed(3).slice(0, -1) + '<br>' +
                                            'Paid    : ' + total + '<br>' +
                                            'Balance : ' + root.sale.salePayment.change;
                                    }

                                    root.$swal({
                                        html: '<pre><h5>' + paid + '</h5></pre>',
                                        type: 'success',
                                        confirmButtonClass: "btn btn-primary",
                                        buttonStyling: false
                                    }).then(function (ok) {
                                        if (ok != null) {
                                            root.Reset(true);
                                        }
                                    });
                                }
                            } else {
                                root.Reset(true);
                            }
                        } else if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Update") {
                            root.loading = false;

                            root.$route.query.data = undefined;
                            if (root.sale.invoiceType == 'Paid') {
                                if (isPrint) {
                                    root.printId = response.data.message.id;
                                    root.PrintRdlc(root.printId);
                                } else {
                                    root.Reset(true);
                                }
                            } else {

                                root.Reset(true);
                            }
                        }
                        else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: response.data.message.isAddUpdate,
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                icon: 'error',
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    })
                    .catch((error) => {
                        console.log(error);
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Something went wrong. Please contact to support",
                            type: 'error',
                            icon: 'error',
                            buttonStyling: false,
                        });
                        root.Reset(true);
                        root.loading = false;
                    })
                    .finally(() => (root.loading = false));
                root.sale.refrenceNo = ""
            },
            PrintRdlc: function (id) {

                var companyId = '';
                companyId = localStorage.getItem('CompanyID');


                var IsDayStart = localStorage.getItem('IsDayStart');
                var CounterId = localStorage.getItem('CounterId');
                this.showReport = true;

                this.reportsrc = this.$ReportServer + '/SalesModuleReports/SaleInvoice/SaleReport.aspx?Id=' + id + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch + '&isReturn=' + false + '&deliveryChallan=' + false + '&simpleQuery=' + false + '&colorVariants=' + this.colorVariants + '&CompanyId=' + companyId + '&formName=SaleRecored' + '&IsDayStart=' + IsDayStart + '&CounterId=' + CounterId
                this.changereport++;

            },
            goToSale: function () {
                if (this.isValid('CanViewHoldInvoices') || this.isValid('CanViewPaidInvoices') || this.isValid('CanViewCreditInvoices')) {
                    this.$router.push({ path: '/SaleService', query: { token_name: 'Sales_token', fromDashboard: 'true' } });

                } else {
                    this.$router.go();
                }
            },

            calcuateSummary: function () {
                this.summary.item = this.saleProducts.filter(prod => prod.quantity > 0).length;


                if (this.decimalQuantity) {
                    this.summary.qty = this.saleProducts.reduce((qty, prod) => qty + (parseFloat(prod.quantity) > 0 ? parseFloat(prod.quantity) : 0), 0);
                    this.summary.qty = parseFloat(this.summary.qty).toFixed(2);
                }
                else {
                    this.summary.qty = this.saleProducts.reduce((qty, prod) => qty + (parseInt(prod.quantity) > 0 ? parseInt(prod.quantity) : 0), 0);
                }

                this.summary.total = this.saleProducts.reduce((total, prod) => total + (prod.grossAmount), 0).toFixed(2);

                var discount = this.saleProducts
                    .filter((x) => x.discount != 0 || x.discount != "")
                    .reduce((discount, prod) => discount + prod.promotionOfferQuantity > 0 ? (prod.promotionOfferQuantity ? (((prod.promotionOfferQuantity * prod.unitPrice) * prod.discount) / 100) : 0) : (prod.quantity ? (((prod.quantity * prod.unitPrice) * prod.discount) / 100) : 0), 0);

                var fixDiscount = this.saleProducts
                    .filter((x) => x.fixDiscount != 0 || x.fixDiscount != "")
                    .reduce((discount, prod) => discount + prod.fixDiscount, 0);

                this.summary.discount = (parseFloat(discount) + parseFloat(fixDiscount)).toFixed(3).slice(0, -1);

                this.summary.withDisc = (this.summary.total - this.summary.discount).toFixed(3).slice(0, -1);

                this.summary.vat = this.saleProducts
                    .reduce((vat, prod) => vat + prod.lineItemVAt, 0).toFixed(3).slice(0, -1);

                //})
                if (this.summary.vat == 'NaN') {
                    this.summary.vat = 0;
                }

                this.summary.withVat = this.saleProducts.reduce((total, prod) => total + (prod.totalAmount), 0).toFixed(3).slice(0, -1);


                if (this.summary.withVat == 'NaN') {
                    this.summary.withVat = 0;
                }

                this.sale.discountAmount = parseFloat(this.summary.discount)
                this.sale.vatAmount = parseFloat(this.summary.vat)
                this.sale.totalAmount = parseFloat(this.summary.withVat)
                this.sale.grossAmount = parseFloat(this.summary.withDisc)
                this.sale.totalAfterDiscount = parseFloat(this.summary.withDisc - this.summary.discount);

                this.$emit("update:modelValue", this.saleProducts);

                this.$emit("summary", this.summary);
            },

            //},
            updateLineTotal: function (e, prop, product, chkForArabic) {


                if (chkForArabic == true) {

                    if (/^[0-9\u0660-\u0669]+$/.test(e) == true) {
                        e = e.replace(/[٠-٩]/g, d => "٠١٢٣٤٥٦٧٨٩".indexOf(d)).replace(/[۰-۹]/g, d => "۰۱۲۳۴۵۶۷۸۹".indexOf(d));

                    } else {
                        e = 0;
                        this.$swal({
                            title: this.$t('AddDailyExpense.Error'),
                            text: "Please Enter valid number in arabic or numeric",
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 1500,
                            timerProgressBar: true,
                        });
                        return;
                    }

                }

                if (e != undefined) {
                    var discount = product.discount == 0 || product.discount == "" ? product.fixDiscount == 0 || product.fixDiscount == "" ?
                        0 :
                        product.fixDiscount :
                        product.discount;

                    var prod = this.products.find((x) => x.id == product.productId);
                    if (prop == "unitPrice") {
                        product.unitPrice = e;
                    }

                    if (product.returnQuantity != undefined && product.returnQuantity > 0) {
                        product.quantity = product.quantity * -1;
                    }
                    if (prop == "quantity") {

                        if (this.sale.isReturnItem && (product.returnQuantity == 0 || product.returnQuantity == '')) {
                            this.removeProduct(product.rowId)
                        } else if (this.sale.isReturnItem && product.returnQuantity > 0) {
                            e = e * -1;
                        } else {
                            if (e < 0) {
                                e = '';
                            }
                        }
                        if (String(e).split('.').length > 1 && String(e).split('.')[1].length > 2)
                            e = parseFloat(String(e).slice(0, -1))
                        product.quantity = this.decimalQuantity ? e : Math.round(e);
                    }

                    if (product.productId != null) {

                        if (product.bundleId != null) {
                            if (product.quantity > product.buy) {
                                product['bundleOffer'] = product.buy.toString() + " + " + product.get.toString()
                                product['isBundleOffer'] = true
                            } else {
                                product['bundleOffer'] = ""
                                product['isBundleOffer'] = false

                            }
                            //bundle category calculation
                            if (parseFloat(product.quantity) >= (product.get + product.buy)) {
                                var offer = Math.floor(parseFloat(product.quantity) / (product.get + product.buy));
                                if ((prod.bundleCategory.quantityOut + offer) <= product.stockLimit) {
                                    if (offer <= product.offerQuantityLimit) {
                                        product.offerQuantity = offer * product.get;
                                    } else {
                                        product.offerQuantity = product.offerQuantityLimit * product.get;
                                    }
                                } else {
                                    product.offerQuantity = 0;
                                    product.discount = 0;
                                    product.fixDiscount = 0;
                                    discount = 0;
                                }
                            } else {
                                product.offerQuantity = 0;
                                product.discount = 0;
                                product.fixDiscount = 0;
                                discount = 0;
                            }
                            //bundle category calculation end
                        }
                    }

                    if (this.invoiceWoInventory == 'false') {
                        var bundleQuantity = product.bundleOfferQuantity == undefined ? 0 : product.bundleOfferQuantity;
                        if (parseFloat(product.quantity) + bundleQuantity > product.currentQuantity) {
                            product['outOfStock'] = true
                        } else {
                            product['outOfStock'] = false
                        }
                    }

                    //End Calculate offer
                    if (prop == "discount") {
                        if (e == "") {
                            e = 0;
                        }
                        product.discount = e;
                    }

                    if (prop == "fixDiscount") {
                        if (e == "") {
                            e = 0;
                        }
                        product.fixDiscount = e;
                    }

                    var vat = 0;
                    var total = 0;
                    var calculateVAt = 0;

                    if (product.promotionId != null) {
                        if (product.promotionType == 'BuyNGetNSameGroup' || product.promotionType == 'BuyNGetNSameItem') {
                            product.promotionName = product.buy + " + " + product.get;
                            if (parseFloat(product.quantity) >= (product.get + product.buy)) {
                                offer = Math.floor(parseFloat(product.quantity) / (product.get + product.buy));
                                if ((product.quantityOut + offer) <= product.stockLimit) {
                                    if (offer <= product.upToQuantity) {
                                        product.offerQuantity = offer * product.get;
                                    } else {
                                        product.offerQuantity = product.upToQuantity * product.get;
                                    }
                                } else {
                                    product.offerQuantity = (product.stockLimit - product.quantityOut) * product.get;
                                }
                            } else {
                                product.offerQuantity = 0;
                                product.discount = 0;
                                product.fixDiscount = 0;
                                discount = 0;
                            }
                            vat = this.vats.find((value) => value.id == product.taxRateId);
                            discount = product.discount == 0 ? (this.taxMethod == ("Inclusive" || "شامل") ? product.fixDiscount + (product.fixDiscount * vat.rate / 100) : product.fixDiscount) : (product.quantity * product.unitPrice * product.discount) / 100;
                            total = (product.quantity - product.offerQuantity) * product.unitPrice - discount;

                            if (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') {
                                calculateVAt = (total * vat.rate) / (100 + vat.rate);
                                product.lineTotal = total;
                                product.lineItemVAt = calculateVAt;
                            } else {
                                calculateVAt = (total * vat.rate) / 100;
                                product.lineTotal = total + calculateVAt;
                                product.lineItemVAt = calculateVAt;
                            }
                            this.saleProducts['product'] = product

                            this.$emit("update:modelValue", this.saleProducts, this.adjustment, this.adjustmentSign, parseFloat(this.transactionLevelDiscount));
                        }

                        if (product.promotionType == 'BuyNGetNAnother') {
                            product.promotionName = product.buy + " + " + product.get;
                            if (parseFloat(product.quantity) >= product.buy) {
                                offer = Math.floor(parseFloat(product.quantity) / product.buy);

                                if (offer <= product.upToQuantity) {
                                    if ((product.quantityOut + offer) <= product.stockLimit) {
                                        product.promotionOfferQuantity = offer * product.get;

                                        var prd = this.saleProducts.find(x => x.productId == product.getProductId);
                                        if (prd != undefined) {
                                            this.updateLineTotal(offer * product.get, "quantity", prd);
                                        } else {
                                            this.getProductFromProductDropdown(product.getProductId, offer * product.get);
                                        }
                                    } else {
                                        product.promotionOfferQuantity = (product.stockLimit - product.quantityOut) * product.get;

                                        prd = this.saleProducts.find(x => x.productId == product.getProductId);
                                        if (prd != undefined) {
                                            this.updateLineTotal((product.stockLimit - product.quantityOut) * product.get, "quantity", prd);
                                        } else {
                                            this.getProductFromProductDropdown(product.getProductId, (product.stockLimit - product.quantityOut) * product.get);
                                        }
                                    }
                                } else {

                                    if ((product.quantityOut + offer) <= product.stockLimit) {
                                        product.promotionOfferQuantity = product.upToQuantity * product.get;

                                        prd = this.saleProducts.find(x => x.productId == product.getProductId);
                                        if (prd != undefined) {
                                            this.updateLineTotal(product.upToQuantity * product.get, "quantity", prd);
                                        } else {
                                            this.getProductFromProductDropdown(product.getProductId, product.upToQuantity * product.get);
                                        }
                                    } else {
                                        product.promotionOfferQuantity = (product.stockLimit - product.quantityOut) * product.get;

                                        prd = this.saleProducts.find(x => x.productId == product.getProductId);
                                        if (prd != undefined) {
                                            this.updateLineTotal((product.stockLimit - product.quantityOut) * product.get, "quantity", prd);
                                        } else {
                                            this.getProductFromProductDropdown(product.getProductId, (product.stockLimit - product.quantityOut) * product.get);
                                        }
                                    }
                                }

                            } else {
                                if (this.saleProducts.find(x => x.productId == product.getProductId) != undefined) {
                                    this.removeProduct(this.saleProducts.find(x => x.productId == product.getProductId).rowId);
                                }
                                product.offerQuantity = 0;
                                product.discount = 0;
                                product.fixDiscount = 0;
                                discount = 0;
                            }

                            vat = this.vats.find((value) => value.id == product.taxRateId);
                            discount = product.discount == 0 ? (this.taxMethod == ("Inclusive" || "شامل") ? product.fixDiscount + (product.fixDiscount * vat.rate / 100) : product.fixDiscount) : (product.quantity * product.unitPrice * product.discount) / 100;

                            total = (product.quantity - product.offerQuantity) * product.unitPrice - discount;
                            if (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') {
                                calculateVAt = (total * vat.rate) / (100 + vat.rate);
                                product.lineTotal = total;
                                product.lineItemVAt = calculateVAt;
                            } else {
                                calculateVAt = (total * vat.rate) / 100;
                                product.lineTotal = total + calculateVAt;
                                product.lineItemVAt = calculateVAt;
                            }

                            this.saleProducts['product'] = product


                            this.$emit("update:modelValue", this.saleProducts, this.adjustment, this.adjustmentSign, parseFloat(this.transactionLevelDiscount));
                        }

                        if (product.promotionType == 'GroupNFixOrPercentageDiscount' || product.promotionType == 'ItemNFixOrPercentageDiscount') {
                            if (prod.promotionOffer.discountType == '%') {
                                product.promotionName = prod.promotionOffer.discount + "%";
                                product.discountSign = '%';
                                if (product.quantity >= product.offerQuantityLimit) {
                                    offer = Math.floor(parseFloat(product.quantity) / product.offerQuantityLimit);
                                    if (offer <= product.upToQuantity) {
                                        if ((product.quantityOut + offer) <= product.stockLimit) {
                                            product.promotionOfferQuantity = offer * product.offerQuantityLimit;
                                            product.discount = prod.promotionOffer.discount;
                                            discount = product.promotionOfferQuantity * product.unitPrice * product.discount / 100;
                                        } else {
                                            product.promotionOfferQuantity = (product.stockLimit - product.quantityOut) * product.offerQuantityLimit;
                                            product.discount = prod.promotionOffer.discount;
                                            discount = product.promotionOfferQuantity * product.unitPrice * product.discount / 100;
                                        }
                                    } else {
                                        if ((product.quantityOut + offer) <= product.stockLimit) {
                                            product.promotionOfferQuantity = product.upToQuantity * product.offerQuantityLimit;
                                            product.discount = prod.promotionOffer.discount;
                                            discount = product.promotionOfferQuantity * product.unitPrice * product.discount / 100;
                                        } else {
                                            product.promotionOfferQuantity = (product.stockLimit - product.quantityOut) * product.offerQuantityLimit;
                                            product.discount = prod.promotionOffer.discount;
                                            discount = product.promotionOfferQuantity * product.unitPrice * product.discount / 100;
                                        }

                                    }
                                } else {
                                    product.discount = 0;
                                    product.fixDiscount = 0;
                                    discount = 0;
                                }

                            } else {
                                product.promotionName = this.currency + ' ' + prod.promotionOffer.discount;
                                product.discountSign = 'F'
                                if (product.quantity >= product.offerQuantityLimit) {
                                    offer = Math.floor(parseFloat(product.quantity) / product.offerQuantityLimit);
                                    if (offer <= product.upToQuantity) {
                                        if ((product.quantityOut + offer) <= product.stockLimit) {
                                            product.fixDiscount = offer * prod.promotionOffer.discount;
                                            discount = product.fixDiscount;
                                            product.discount = 0;
                                            product.promotionOfferQuantity = offer;
                                        } else {
                                            product.fixDiscount = (product.stockLimit - product.quantityOut) * prod.promotionOffer.discount;
                                            discount = product.fixDiscount;
                                            product.discount = 0;
                                            product.promotionOfferQuantity = offer;
                                        }

                                    } else {
                                        if ((product.quantityOut + offer) <= product.stockLimit) {
                                            product.fixDiscount = product.upToQuantity * prod.promotionOffer.discount;
                                            discount = product.fixDiscount;
                                            product.discount = 0;
                                            product.promotionOfferQuantity = product.upToQuantity;
                                        } else {
                                            product.fixDiscount = (product.stockLimit - product.quantityOut) * prod.promotionOffer.discount;
                                            discount = product.fixDiscount;
                                            product.discount = 0;
                                            product.promotionOfferQuantity = product.upToQuantity;
                                        }

                                    }
                                } else {
                                    product.discount = 0;
                                    product.fixDiscount = 0;
                                    discount = 0;
                                }
                            }

                            vat = this.vats.find((value) => value.id == product.taxRateId);
                            total = product.quantity * product.unitPrice;
                            if (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') {
                                calculateVAt = product.discountSign == 'F' ? (((total * 100) / (100 + vat.rate)) - discount) * vat.rate / 100 : (total - discount) * vat.rate / (vat.rate + 100);
                                product.lineItemVAt = calculateVAt;
                                product.lineTotal = product.discountSign == 'F' ? ((total * 100 / (100 + vat.rate)) - discount) * (100 + vat.rate) / 100 : total - discount;
                            } else {
                                calculateVAt = ((total - discount) * vat.rate) / 100;
                                product.lineItemVAt = calculateVAt;
                                product.lineTotal = (total + calculateVAt) - discount;
                            }
                            this.saleProducts['product'] = product



                            this.$emit("update:modelValue", this.saleProducts, this.adjustment, this.adjustmentSign, parseFloat(this.transactionLevelDiscount));
                        }
                    } else if (product.isBundleOffer) {
                        //isDiscountBeforeVat
                        vat = this.vats.find((value) => value.id == product.taxRateId);
                        product.fixDiscount = product.offerQuantity * product.unitPrice;
                        discount = this.taxMethod == ("Inclusive" || "شامل") ? product.fixDiscount + (product.fixDiscount * vat.rate / 100) : product.fixDiscount;
                        product.lineItemVAt = this.taxMethod == ("Inclusive" || "شامل") ? (product.quantity - product.offerQuantity) * product.unitPrice * vat.rate / (100 + vat.rate) : (product.quantity - product.offerQuantity) * product.unitPrice * vat.rate / 100;
                        product.lineTotal = (product.quantity * product.unitPrice) - discount;

                        this.saleProducts['product'] = product



                        this.$emit("update:modelValue", this.saleProducts);
                    }
                    else {

                        discount = product.discount == 0 ? (product.fixDiscount * product.quantity) : (product.quantity * product.unitPrice * product.discount) / 100;
                        vat = this.vats.find((value) => value.id == product.taxRateId);

                        total = product.quantity * product.unitPrice - discount;
                        if (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') {
                            calculateVAt = (total * vat.rate) / (100 + vat.rate);
                            /*product.lineTotal = total - calculateVAt;*/
                            product.lineTotal = total;
                            product.lineItemVAt = calculateVAt;
                        } else {
                            calculateVAt = (total * vat.rate) / 100;
                            product.lineTotal = total + calculateVAt;
                            product.lineItemVAt = calculateVAt;
                        }

                        this.saleProducts['product'] = product

                        this.$emit("update:modelValue", this.saleProducts);
                    }
                    product.discountAmount = discount;
                    product.vatAmount = product.lineItemVAt;
                    product.totalAmount = product.lineTotal;
                    product.grossAmount = (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') ? ((product.quantity - product.offerQuantity) * product.unitPrice) * 100 / (100 + vat.rate) : (product.quantity - product.offerQuantity) * product.unitPrice;
                    //this.productRemainingQuantity(product.productId, product.quantity);
                    this.quantity = product.quantity;
                    this.quantityRander++;
                    this.calcuateSummary();
                }
            },

            getProductFromProductDropdown: function (productId, qty) {
                var newProduct = this.productList.find(x => x.id == productId);

                if (this.products.find(x => x.id == productId) == undefined || this.products.length <= 0) {
                    this.products.push(newProduct);
                }
                var prod = this.products.find((x) => x.id == productId);

                var rate = this.getVatValue(prod.taxRateId, prod);

                var uid = this.createUUID();
                this.saleProducts.push({
                    rowId: uid,
                    productId: prod.id,
                    description: prod.englishName,
                    unitPrice: '0',
                    quantity: qty,
                    soQty: 0,
                    highQty: 0,

                    discount: 0,
                    fixDiscount: 0,
                    discountSign: '%',

                    promotionId: prod.promotionOffer == null ? null : prod.promotionOffer.id,
                    bundleId: prod.bundleCategory == null ? null : prod.bundleCategory.id,
                    promotionName: '',
                    promotionType: prod.promotionOffer != null ? prod.promotionOffer.promotionType : null,
                    buy: prod.bundleCategory != null ? prod.bundleCategory.buy : prod.promotionOffer != null ? prod.promotionOffer.buy : 0,
                    get: prod.bundleCategory != null ? prod.bundleCategory.get : prod.promotionOffer != null ? prod.promotionOffer.get : 0,
                    offerQuantityLimit: prod.bundleCategory != null ? prod.bundleCategory.quantityLimit : (prod.promotionOffer != null ? prod.promotionOffer.baseQuantity : 0),
                    stockLimit: prod.bundleCategory != null ? prod.bundleCategory.stockLimit : (prod.promotionOffer != null ? prod.promotionOffer.stockLimit : 0),
                    quantityOut: prod.bundleCategory != null ? prod.bundleCategory.quantityOut : (prod.promotionOffer != null ? prod.promotionOffer.quantityOut : 0),
                    upToQuantity: prod.promotionOffer == null ? 0 : prod.promotionOffer.upToQuantity,
                    includingBaseQuantity: prod.promotionOffer == null ? false : prod.promotionOffer.includingBaseQuantity,
                    getProductId: prod.promotionOffer == null ? '' : prod.promotionOffer.getProductId,
                    offerQuantity: 0,
                    promotionOfferQuantity: 0,

                    lineItemVAt: 0,
                    grossAmount: 0,
                    batchExpiry: prod.batchExpiry,
                    batchNo: prod.batchNo,
                    inventoryList: prod.inventoryBatch == null ? null : prod.inventoryBatch,
                    currentQuantity: prod.inventory == null ? 0 : prod.inventory.currentQuantity,
                    taxRateId: prod.taxRateId,
                    taxMethod: prod.taxMethod,
                    saleReturnDays: prod.saleReturnDays,
                    scheme: prod.scheme,
                    schemeQuantity: prod.schemeQuantity,
                    wholesalePrice: prod.wholesalePrice,
                    salePrice: prod.salePrice,
                    wholesaleQuantity: prod.wholesaleQuantity,
                    schemePhysicalQuantity: 0,
                    rate: rate,
                    serialNo: '',
                    serial: '',
                    guaranteeDate: '',
                    isSerial: newProduct.serial,
                    guarantee: newProduct.guarantee,
                    lineTotal: prod.salePrice * 1,
                    unitPerPack: newProduct.unitPerPack,
                    levelOneUnit: prod.levelOneUnit,
                    basicUnit: prod.basicUnit,
                    productSizes: prod.productSizes,
                    colorId: '',
                    colorName: '',
                    isFree: true,
                    highUnitPrice: newProduct.highUnitPrice,
                });

                var product = this.saleProducts.find((x) => {
                    return x.productId == productId && x.rowId == uid;
                });

                this.getVatValue(product.taxRateId, product);

                this.updateLineTotal(product.quantity, "quantity", product);
                this.updateLineTotal(product.highQty, "highQty", product);

                this.product.id = "";
                this.rendered++;
            },

            addProduct: function (productId, newProduct, quantitychange) {
                

                if (this.isOldInvoice) {
                    this.sale.returnInvoiceAmount = this.summary.withVat
                    this.isOldInvoice = false
                }
                this.styleclass = productId;
                if (this.styleclass == productId) {
                    this.isCardColor = true;
                }

                if (quantitychange != null || quantitychange != undefined) {
                    this.isSaleReturn = quantitychange;
                }
                this.products.push(newProduct);
                if (this.saleProducts.some(x => x.productId == productId)) {
                    var prd = this.saleProducts.find(x => x.productId == productId);
                    if (quantitychange != undefined || quantitychange != null) {
                        if (!this.isSaleReturn) {
                            prd.quantity--;
                        }
                        if (this.isSaleReturn) {
                            prd.quantity++;
                        }
                    }

                    this.updateLineTotal(prd.quantity, "quantity", prd);
                }
                else {

                    if (this.products.find(x => x.id == newProduct.id) == undefined || this.products.length <= 0) {
                        this.products.push(newProduct);
                    }
                    var prod = this.products.find((x) => x.id == productId);

                    var rate = 0;
                    var taxRateId = '';
                    var taxMethod = '';
                    if (this.saleDefaultVat == 'DefaultVat' || this.saleDefaultVat == 'DefaultVatItem') {
                        if (prod.taxRateId != "00000000-0000-0000-0000-000000000000" && prod.taxRateId != undefined) {
                            rate = this.getVatValue(prod.taxRateId, prod);
                        }
                        taxRateId = prod.taxRateId;
                        taxMethod = prod.taxMethod;
                    }
                    if (this.saleDefaultVat == 'DefaultVatHead' || this.saleDefaultVat == 'DefaultVatHeadItem') {
                        if (this.taxRateId != "00000000-0000-0000-0000-000000000000" && this.taxRateId != undefined) {

                            rate = this.getVatValue(this.taxRateId, prod);
                        }

                        taxRateId = this.taxRateId;
                        taxMethod = this.taxMethod;
                    }

                    /*var rate = this.getVatValue(prod.taxRateId, prod);*/

                    if (this.isSaleReturn) {
                        this.saleProducts.push({
                            rowId: this.createUUID(),
                            productId: prod.id,
                            productName: prod.displayName,
                            code: prod.code,
                            unitPrice: prod.salePrice,
                            quantity: -1,
                            discount: 0,
                            fixDiscount: 0,
                            taxRateId: taxRateId,
                            promotionId: prod.promotionOffer == null ? null : prod.promotionOffer.id,
                            bundleId: prod.bundleCategory == null ? null : prod.bundleCategory.id,
                            saleReturnDays: prod.saleReturnDays,
                            rate: rate,
                            taxMethod: taxMethod,
                            lineTotal: prod.salePrice * 1,
                            buy: prod.bundleCategory != null ? prod.bundleCategory.buy : 0,
                            get: prod.bundleCategory != null ? prod.bundleCategory.get : 0,
                            quantityLimit: prod.bundleCategory != null ? prod.bundleCategory.quantityLimit : 0,
                            offerQuantity: 0,
                            vatAmount: 0,
                            discountAmount: 0,
                            currentQuantity: 0,
                            totalAmount: 0,
                            grossAmount: 0,
                        });
                    }
                    if (!this.isSaleReturn) {

                        this.saleProducts.push({
                            rowId: this.createUUID(),
                            productId: prod.id,
                            productName: prod.displayName,
                            code: prod.code,
                            unitPrice: prod.salePrice == 0 ? '0' : prod.salePrice,
                            quantity: 1,
                            discount: 0,
                            fixDiscount: 0,

                            promotionId: prod.promotionOffer == null ? null : prod.promotionOffer.id,
                            bundleId: prod.bundleCategory == null ? null : prod.bundleCategory.id,
                            promotionName: '',
                            promotionType: prod.promotionOffer != null ? prod.promotionOffer.promotionType : null,
                            buy: prod.bundleCategory != null ? prod.bundleCategory.buy : prod.promotionOffer != null ? prod.promotionOffer.buy : 0,
                            get: prod.bundleCategory != null ? prod.bundleCategory.get : prod.promotionOffer != null ? prod.promotionOffer.get : 0,
                            offerQuantityLimit: prod.bundleCategory != null ? prod.bundleCategory.quantityLimit : (prod.promotionOffer != null ? prod.promotionOffer.baseQuantity : 0),
                            stockLimit: prod.bundleCategory != null ? prod.bundleCategory.stockLimit : (prod.promotionOffer != null ? prod.promotionOffer.stockLimit : 0),
                            quantityOut: prod.bundleCategory != null ? prod.bundleCategory.quantityOut : (prod.promotionOffer != null ? prod.promotionOffer.quantityOut : 0),
                            upToQuantity: prod.promotionOffer == null ? 0 : prod.promotionOffer.upToQuantity,
                            includingBaseQuantity: prod.promotionOffer == null ? false : prod.promotionOffer.includingBaseQuantity,
                            getProductId: prod.promotionOffer == null ? '' : prod.promotionOffer.getProductId,
                            offerQuantity: 0,
                            promotionOfferQuantity: 0,

                            taxRateId: prod.taxRateId,
                            batchExpiry: prod.inventoryBatch == null || prod.inventoryBatch.length == 0 ? '' : prod.inventoryBatch[0].expiryDate,
                            batchNo: prod.inventoryBatch == null || prod.inventoryBatch.length == 0 ? '' : prod.inventoryBatch[0].batchNumber,
                            inventoryList: prod.inventoryBatch == null ? null : prod.inventoryBatch,
                            taxMethod: prod.taxMethod,
                            saleReturnDays: prod.saleReturnDays,
                            rate: rate,
                            vatAmount: 0,
                            discountAmount: 0,
                            totalAmount: 0,
                            grossAmount: 0,
                            currentQuantity: 0,
                            lineTotal: prod.salePrice * 1,
                            quantityLimit: prod.bundleCategory != null ? prod.bundleCategory.quantityLimit : 0,
                        });

                        this.GetProductInfo(productId);
                    }
                }
                var product = this.saleProducts.find((x) => {
                    return x.productId == productId;
                });

                this.getVatValue(product.taxRateId, product);
                this.updateLineTotal(product.quantity, "quantity", product);

                this.product.id = "";
                this.rendered++;
            },

            GetProductInfo: function (id) {
                var root = this;
                var token = localStorage.getItem('token');

                var openBatch;
                var batch = localStorage.getItem('openBatch')
                if (batch != undefined && batch != null && batch != "") {
                    openBatch = batch
                }
                else {
                    openBatch = 1
                }

                root.$https.get('/Product/GetProductDetailForInvoiceQuery?id=' + id + '&wareHouseId=' + this.sale.wareHouseId + "&isFifo=" + root.isFifo + "&openBatch=" + openBatch + '&branchId=' + localStorage.getItem('BranchId'), { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        var newProduct = response.data;
                        var prod = root.saleProducts.find((x) => x.productId == id);

                        if (prod != undefined) {
                            prod.promotionId = newProduct.promotionOffer == null ? null : newProduct.promotionOffer.id;
                            prod.bundleId = newProduct.bundleCategory == null ? null : newProduct.bundleCategory.id;
                            prod.promotionName = '';
                            prod.promotionType = newProduct.promotionOffer != null ? newProduct.promotionOffer.promotionType : null;
                            prod.buy = newProduct.bundleCategory != null ? newProduct.bundleCategory.buy : prod.promotionOffer != null ? newProduct.promotionOffer.buy : 0;
                            prod.get = newProduct.bundleCategory != null ? newProduct.bundleCategory.get : prod.promotionOffer != null ? newProduct.promotionOffer.get : 0;
                            prod.offerQuantityLimit = newProduct.bundleCategory != null ? newProduct.bundleCategory.quantityLimit : (newProduct.promotionOffer != null ? newProduct.promotionOffer.baseQuantity : 0);
                            prod.stockLimit = newProduct.bundleCategory != null ? newProduct.bundleCategory.stockLimit : (newProduct.promotionOffer != null ? newProduct.promotionOffer.stockLimit : 0);
                            prod.quantityOut = newProduct.bundleCategory != null ? newProduct.bundleCategory.quantityOut : (newProduct.promotionOffer != null ? newProduct.promotionOffer.quantityOut : 0);
                            prod.upToQuantity = newProduct.promotionOffer == null ? 0 : newProduct.promotionOffer.upToQuantity;
                            prod.includingBaseQuantity = newProduct.promotionOffer == null ? false : newProduct.promotionOffer.includingBaseQuantity;
                            prod.getProductId = newProduct.promotionOffer == null ? '' : newProduct.promotionOffer.getProductId;
                            prod.offerQuantity = 0;
                            prod.promotionOfferQuantity = 0;
                            prod.currentQuantity = newProduct.inventory == null ? 0 : newProduct.inventory.currentQuantity;
                            prod.batchExpiry = newProduct.inventoryBatch == null || newProduct.inventoryBatch.length == 0 ? '' : newProduct.inventoryBatch[0].expiryDate;
                            prod.batchNo = newProduct.inventoryBatch == null || newProduct.inventoryBatch.length == 0 ? '' : newProduct.inventoryBatch[0].batchNumber;
                            prod.inventoryList = newProduct.inventoryBatch == null ? null : newProduct.inventoryBatch;
                            prod.saleReturnDays = newProduct.saleReturnDays;

                            prod.quantityLimit = newProduct.bundleCategory != null ? newProduct.bundleCategory.quantityLimit : 0;


                            root.updateLineTotal(prod.quantity, "quantity", prod);
                        }
                    }
                });
            },


            createUUID: function () {
                var dt = new Date().getTime();
                var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                    var r = (dt + Math.random() * 16) % 16 | 0;
                    dt = Math.floor(dt / 16);
                    return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
                });
                return uuid;
            },

            GetHeaderDetail: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get("/Company/GetCompanyDetail?id=" + root.companyId, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    },
                })
                    .then(function (response) {
                        if (response.data != null) {
                            root.headerFooter.company = response.data;
                            if (root.overWrite) {

                                root.headerFooter.company.nameArabic = root.BusinessNameArabic;
                                root.headerFooter.company.nameEnglish = root.BusinessNameEnglish;
                                root.headerFooter.company.categoryArabic = root.BusinessTypeArabic;
                                root.headerFooter.company.categoryEnglish = root.BusinessTypeEnglish;

                                root.headerFooter.company.companyNameArabic = root.CompanyNameArabic;
                                root.headerFooter.company.companyNameEnglish = root.CompanyNameEnglish;
                                root.headerFooter.company.logoPath = root.BusinessLogo;
                            }
                            root.GetInvoicePrintSetting();
                            root.getBase64Image(root.headerFooter.company.logoPath);
                        }
                    });
            },
            GetInvoicePrintSetting: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var branchId = localStorage.getItem('BranchId');

                root.$https.get('/Sale/printSettingDetail?branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null && response.data != '') {

                            root.headerFooter.footerEn = response.data.termsInEng;
                            root.headerFooter.footerAr = response.data.termsInAr;
                            root.headerFooter.isHeaderFooter = response.data.isHeaderFooter;
                            root.headerFooter.englishName = response.data.englishName;
                            root.headerFooter.arabicName = response.data.arabicName;
                            root.headerFooter.customerAddress = response.data.customerAddress;
                            root.headerFooter.customerVat = response.data.customerVat;
                            root.headerFooter.customerNumber = response.data.customerNumber;
                            root.headerFooter.cargoName = response.data.cargoName;
                            root.headerFooter.customerTelephone = response.data.customerTelephone;
                            root.headerFooter.itemPieceSize = response.data.itemPieceSize;
                            root.headerFooter.styleNo = response.data.styleNo;
                            root.headerFooter.blindPrint = response.data.blindPrint;
                            root.headerFooter.showBankAccount = response.data.showBankAccount;
                            root.headerFooter.bankAccount1 = response.data.bankAccount1;
                            root.headerFooter.bankAccount2 = response.data.bankAccount2;
                            root.headerFooter.bankIcon1 = response.data.bankIcon1;
                            root.headerFooter.bankIcon2 = response.data.bankIcon2;
                            root.headerFooter.customerNameEn = response.data.customerNameEn;
                            root.headerFooter.customerNameAr = response.data.customerNameAr;
                            root.headerFooter.exchangeDays = response.data.exchangeDays;
                            root.headerFooter.printOptions = response.data.printOptions;
                            root.headerFooter.invoicePrint = response.data.invoicePrint;
                            root.headerFooter.welcomeLineEn = response.data.welcomeLineEn;
                            root.headerFooter.welcomeLineAr = response.data.welcomeLineAr;
                            root.headerFooter.closingLineEn = response.data.closingLineEn;
                            root.headerFooter.closingLineAr = response.data.closingLineAr;
                            root.headerFooter.contactNo = response.data.contactNo;
                            root.headerFooter.managementNo = response.data.managementNo;
                            root.headerFooter.businessAddressArabic = response.data.businessAddressArabic;
                            root.headerFooter.businessAddressEnglish = response.data.businessAddressEnglish;
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            },

            getBase64Image: function (path) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.headerFooter.company.logoPath = 'data:image/png;base64,' + 'iVBORw0KGgoAAAANSUhEUgAAATYAAACjCAMAAAA3vsLfAAAAhFBMVEX///8AcuEAZ98Ab+AAa+AAbeAAad9Tk+cAbuC+1fUFeOLS4/no8/0AZd8AaODv9v1to+vk7/z1+v7d6vqBru3F2vemxfKHsu5Zl+iYvfDU5PnM3/g9iea20PWNte5Rk+gwhOVIjuepx/NjnOl2p+wlfuS30fUAX96cvvAde+M1huWoxfIDNGHbAAAHKElEQVR4nO2d65aiOhBGJQlpVAKId6VVtJ3ROe//fgfvqSQocS3EjrV/NsEJ38qlqlKVabUQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEGQ30E6nG6ybcfrbPNkN2+6N68kDWVimzdHOYlYEFDP8yj1GWGT79q6+W7kQoZWfi/8IfwgmEzA/UVaY1/fiCX49k7V1wbC90wwMq2zt2/D1zOyDSkzinaAb/v19vgteEa2hSgV7TBVxQcscU/IlpB7qhUI9yeqvWxJ9EC1QrdR7f1uGGvZpndn6EW3Yf09bxRb2WZVVCsWOMcNEVvZtqqxZsafvKDvDWIp245XUq2Ypm77WpayaWONRYIxwTXjl2av6H1j2Mm2VgYb88bzwo8Nhz9MFU50X9L/hrCTbQVHmxhcn8QbxS7xk3o73ixWsvXgNgrdgYViBbM6u900VrLBORrt4NMf6KgSlzcFK9k2gdRWX/SV0eayi2Ul215uTLQ1/xsMxmBTV5/fACvZ5KWNGtoC2eiqlg6/BzayxbJs/o/eIAc/Vj1U/PuwkQ1spGynNxgA443X1OV3wEa2viwbNwQjFwxl0wGjja/1BlA2lw23p9c2k33xAz2smrr8DljtpLL/ZLIvgO8VuOzMW8kGZNE3yhi4V047pVaygUnI/6iPp3BHcPlAwUo24AZo9m4Ko0rC5fNSK9nAnuAxZRZmAZDN5R3BMt4G3QAykJ9NYACEjWvsdeMosg3bJZxO8Nowpsaz60Scb5X8BtFr7JteAJTN42X8PeVwdWBzKrLdcDZrL1ZCOWVwOv6hyVYKP8n2Rz0mpYyTiDPtV9webLaytfLgcdtDc6dXNnvZwvIULQm6bfarasdWtla7SjYDc3uKPiFba/dYN8eP5FvPyNYaPdCNEtfzjZ6SrTWM7u0LzHPZqzrzjGytOFfNtNtQEy4HPq48JVvh1Hci04tUrJxf1o48KVurtV4JxcgNmMjbjXzE61lWlC3SC2Rm41UUMRYU+IWvwLKp62bHjaUgMtERyRdlB/wCY1ZpPByNk8lkkizWXcfTTiHxrKB/otfrnUuvIGlB0/1EnKQ3/16P1t/DiutUf3hsPv+cZU2nu8gOYaADEWHL0QMt5uMVuTbn+XT2ml6+F+HCIwwksBUGRbmbFI6L5uAAkJHO7tPWwDiJDCEhKnKzpxQmxNScs/FHCTfVU+TPNiwxZBaVN/cY/YD6vjNxdqf8jGinAnebe8LtU4QbM/9upJsvYfN+cD8wzrY2Jfe/lpnRK5d1+LJq7gXeB+hW4XQgWsjNH3uxgetHCa1qNXtiZtXcY86vb4MqNXvByqq5WiDjHn218D1gJGJcMeLo37MM/WpVuMUbbttvE7grUpKNZnEaz8e+L/1xewk/fmmbKPUZ8/WJyxZ3/9lfjjJ62PYa004nZ+OMRvvrjNNKvhnPB9Ppzz/dxWAuD7cBzO0Gpdrj4/yNOtIylUDngPKLExqO1UMZp5MpgQx+Dh8mzOMeyKOHY8pfSfZZ31OSkVYv6H5DdOGGoD72oGitYXRHmFiZpSKstetNApKUIy2HWY1+jGFOs+ILdOHCZyqTcQRQIHrNQO13z8xmXYkZzEHV6zngyucP1OfOIJv8t8/cwNOsM6IDV0KubZXQBqS5+twZ5EnHr0fDiTGYRv/BetKl/nMgRZXuX/klrwQkzJPrSlYi2xLWXBns2QQYw86WqoWyDtF15zPLFmxCuwo/ZwsjgQ7kgWz+IHxUTzr9DNlSINs1OGSWje3AJOX/6b8HDRT/lZ/yUkDp2XX4mGWLhmBLYAb7Al544W6sUq50vBUylsjWAxd9mJwn0D5w9y4ysPVdJ5VZNk9JhiPamf0c+F4OF12N5Fgtv5j9RtkOgwcs+fqNFrCQjbubIgjDbZe9dPP3dOczkWU4GBzQ81evGlvDuIBwOOAG7frO6UvjS5ZbJj09mnVwAHIQ45hDT97kRTgDtLRoRxYizXxVBWhhUCZl1ihjzRBPcYgYpiVQPr1MrXQHzupPJRqhog2ZnILoaVvLb6h8PfmvRLl0zeP+ZtQeDkdJYLznKVGaB1GwWi5XvnZSHxl8L4eItbOTgPGIc+WE6nL/sN7cowXaH50/lx89uiH8wO0aXXUJK0E4n1mZl6WqSbBb/PurSkEpcfqU9EjqPczqAIWO28f1y9xdv+pGaFiboGpgeU/3j4anmhDnKOH+7sQTyqaYZvfTZz6jxu/ApjwjJmB6tvigtC7ykOzr8nG8wndgHnBU5KbMyPm2JKWyaP9RhR3pmGv/AZNHyb7MRVp3iL41+GTlbtijhHS3F1I5R+BzsrwnQvuLcKk9ZRFPnL5+vZT+buKJw9myECwbtB9FftL2IGPn9nS5cP9igTukvUOJZPVY2bF96HBsDUEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQmf8BckJbXFsgizoAAAAASUVORK5CYII=';

                {

                    root.$https
                        .get('/Contact/GetBaseImage?filePath=' + path, {
                            headers: {
                                "Authorization": `Bearer ${token}`
                            }
                        }).then(function (response) {

                            if (response.data != null) {
                                root.filePath = response.data;
                                root.headerFooter.company.logoPath = 'data:image/png;base64,' + root.filePath;
                            }

                        });
                }

            },
            PrintCashVoucher: function (id) {


                var companyId = '';
                companyId = localStorage.getItem('CompanyID');

                var IsDayStart = localStorage.getItem('IsDayStart');
                var CounterId = localStorage.getItem('CounterId');
                this.showReport = true;

                this.reportsrc = this.$ReportServer + '/SalesModuleReports/SaleInvoice/SaleReport.aspx?Id=' + id + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch + '&isReturn=' + false + '&deliveryChallan=' + false + '&simpleQuery=' + false + '&colorVariants=' + false + '&CompanyId=' + companyId + '&formName=SaleRecored' + '&IsDayStart=' + IsDayStart + '&CounterId=' + CounterId
                this.changereport++;
                this.show = !this.show;

            },
            PrintInvoice: function (id) {
                var companyId = '';
                companyId = localStorage.getItem('CompanyID');
                this.showReport = true;
                this.reportsrc = this.$ReportServer + '/SalesModuleReports/SaleInvoice/SaleReport.aspx?Id=' + id + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch + '&isReturn=' + false + '&deliveryChallan=' + false + '&simpleQuery=' + false + '&colorVariants=' + this.colorVariants + '&CompanyId=' + companyId + '&formName=SaleRecored'
                this.changereport++;
                this.show = !this.show;
            },
            PrintInvoiceA4: function (value) {

                var id = value;
                var root = this;
                var token = '';

                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.printDetailsPos = [];
                root.printDetailsCashVoucher = [];
                root.$https.get("/Sale/SaleDetail?id=" + id, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    },
                })
                    .then(function (response) {
                        if (response.data != null) {
                            root.printDetails = response.data;
                            root.printRender++;
                        }
                    });
            },

            CounterDetail: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                //eslint-disable-line
                if (root.counterCode == 'null' || root.counterCode == 'undefined' || root.counterCode == null || root.counterCode == undefined) {
                    this.$https.get('/Product/OpeningBalance?counterId=' + this.sale.counterId + '&isOpeningCash=' + false, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    }).then(function (response) {

                        if (response.data != null) {
                            root.counterCode = response.data.terminalCode;
                            localStorage.setItem('CounterCode', root.counterCode);
                        }
                    }).catch((error) => {
                        console.log(error);
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Something went wrong. Please contact to support",
                            type: 'error',
                            icon: 'error',
                            buttonStyling: false,
                        });
                        root.Reset(true);
                        root.loading = false;
                    })
                        .finally(() => (root.loading = false));
                }
            },
        },
        created: function () {
            var root = this;

            if (root.$route.query.Add == 'false') {
                root.$route.query.data = this.$store.isGetEdit;
            }
            

            root.counterCode = localStorage.getItem('CounterCode');
            root.dayStartTime = localStorage.getItem('DayStartTime');
            this.isFifo = localStorage.getItem('fIFO') == 'true' ? true : false;
            var batch = localStorage.getItem('openBatch')
            if (batch != undefined && batch != null && batch != "") {
                this.openBatch = batch
            } else {
                this.openBatch = 1
            }

            root.headerFooter.returnDays = localStorage.getItem('ReturnDays');
            root.headerFooter.footerEn = localStorage.getItem('TermsInEng');
            root.headerFooter.footerAr = localStorage.getItem('TermsInAr');
            root.headerFooter.printSizeA4 = localStorage.getItem('PrintSizeA4');
            this.companyId = localStorage.getItem('CompanyID');
            if (this.$route.query.data != undefined) {
                var data = this.$route.query.data;
                this.sale.id = data.id;
                this.sale.wareHouseId = data.wareHouseId;
                this.sale.invoiceType = data.invoiceType;
                this.sale.cashCustomer = data.cashCustomer;
                this.sale.date = moment(data.date).format("LLL");
                this.sale.dueDate = moment(data.dueDate).format("LLL");
                this.sale.registrationNo = data.registrationNo;
                this.sale.mobile = data.mobile;
                this.sale.code = data.code;
                this.sale.customerId = data.customerId;
                this.sale.saleItems = data.saleItems;
                this.sale.salePayment = {
                    dueAmount: 0,
                    received: 0,
                    balance: 0,
                    change: 0,
                    paymentTypes: [],
                    paymentType: 'Cash',
                    paymentOptionId: '',
                };
            } else {
                this.sale.wareHouseId = localStorage.getItem('WareHouseId');
            }
            this.getData();
            if (this.$route.query.mobiledata != undefined) {
                for (var j = 0; j < this.$route.query.mobiledata.mobileOrderItemLookupModels.length; j++) {

                    this.saleProducts.quantity[j] = this.$route.query.mobiledata.mobileOrderItemLookupModels[j].quantity[j];

                }
                root.calcuateSummary();
                this.saleProducts.rowId = this.$route.query.mobiledata.mobileOrderItemLookupModels.rowId;
                this.saleProducts.quantity = this.$route.query.mobiledata.mobileOrderItemLookupModels.quantity;
            }
            this.itemRender++;
        },
        mounted: function () {
            var root = this;
            this.saleDefaultVat = localStorage.getItem('SaleDefaultVat');
            this.taxRateId = localStorage.getItem('TaxRateId');

            this.overWrite = localStorage.getItem('overWrite') == 'true' ? true : false;
            this.taxMethod = localStorage.getItem('taxMethod');
            this.sale.taxRateId = localStorage.getItem('TaxRateId');
            this.BusinessLogo = localStorage.getItem('BusinessLogo');
            this.BusinessNameArabic = localStorage.getItem('BusinessNameArabic');
            this.BusinessNameEnglish = localStorage.getItem('BusinessNameEnglish');
            this.BusinessTypeArabic = localStorage.getItem('BusinessTypeArabic');
            this.BusinessTypeEnglish = localStorage.getItem('BusinessTypeEnglish');
            this.CompanyNameArabic = localStorage.getItem('CompanyNameArabic');
            this.CompanyNameEnglish = localStorage.getItem('CompanyNameEnglish');
            this.GetHeaderDetail();

            this.bankDetail = localStorage.getItem('BankDetail') == 'true' ? true : false;
            this.decimalQuantity = localStorage.getItem('decimalQuantity') == 'true' ? true : false;
            this.loginUserName = localStorage.getItem('LoginUserName');
            var IsDayStart = localStorage.getItem('DayStart');
            var IsExpenseDay = localStorage.getItem('IsExpenseDay');
            var IsDayStartOn = localStorage.getItem('IsDayStart');
            this.currency = localStorage.getItem('currency');
            this.invoiceWoInventory = localStorage.getItem('InvoiceWoInventory');
            this.cashVoucherAllow = localStorage.getItem('CashVoucher');
            this.sale.dayStart = IsDayStart == 'false' ? false : true;
            this.sale.counterId = localStorage.getItem('CounterId');
            this.printTemplate = localStorage.getItem('PrintTemplate');
            this.printSize = localStorage.getItem('PrintSizeA4');
            if (this.sale.bankCashAccountId == '') {
                if (localStorage.getItem('CashAccountId') != null && localStorage.getItem('CashAccountId') != undefined && localStorage.getItem('CashAccountId') != '' && localStorage.getItem('CashAccountId') != 'null' && localStorage.getItem('CashAccountId') != "null" && localStorage.getItem('CashAccountId') != "00000000-0000-0000-0000-000000000000" && localStorage.getItem('CashAccountId') != '00000000-0000-0000-0000-000000000000') {

                    this.sale.bankCashAccountId = localStorage.getItem('CashAccountId');
                }
            }

            if (IsDayStart != 'true') {
                this.isDayAlreadyStart = true;
                this.getBank();

                if (localStorage.getItem('OnPageLoadItem') == 'true') {
                    root.getProduct(this.categoryId);
                }
                this.getCurrency();
                this.GetCategoryData(this.search, true);
                if (this.$route.query.data == undefined) {
                    this.sale.date = moment().format("LLL");
                    this.sale.dueDate = moment().format("LLL");
                }
                this.AutoIncrementCode();

                if (this.$route.query.data != undefined) {
                    this.warehouse = this.$route.query.data;
                }
                if (this.$route.query.mobiledata != undefined) {
                    this.sale.date = this.$route.query.mobiledata.orderDate;
                }
                this.changePriceDuringSale = localStorage.getItem('changePriceDuringSale');
                this.changePriceDuringSale == 'true' ? (this.changePriceDuringSale = true) : (this.changePriceDuringSale = false);
                this.giveDiscountDuringSale = localStorage.getItem('giveDicountDuringSale');
                this.giveDiscountDuringSale == 'true' ? (this.giveDiscountDuringSale = true) : (this.giveDiscountDuringSale = false);
                root.CounterDetail();
            } else {
                if (IsExpenseDay == 'true') {
                    this.isDayAlreadyStart = false;
                } else {
                    this.CompanyID = localStorage.getItem('CompanyID');
                    this.UserID = localStorage.getItem('UserID');
                    this.employeeId = localStorage.getItem('EmployeeId');
                    if (IsDayStartOn == 'true') {
                        this.isDayAlreadyStart = true;
                        root.getBank();

                        if (localStorage.getItem('OnPageLoadItem') == 'true') {
                            root.getProduct(this.categoryId);
                        }

                        root.getCurrency();
                        root.GetCategoryData(root.search, true);
                        root.invoiceWoInventory = localStorage.getItem('InvoiceWoInventory');
                        if (root.$route.query.data == undefined) {
                            root.sale.date = moment().format("LLL");
                            root.sale.dueDate = moment().format("LLL");
                        }
                        root.AutoIncrementCode();
                        if (root.$route.query.data != undefined) {
                            root.warehouse = root.$route.query.data;
                        }
                        if (root.$route.query.mobiledata != undefined) {
                            root.sale.date = root.$route.query.mobiledata.orderDate;
                        }
                        root.changePriceDuringSale = localStorage.getItem('changePriceDuringSale');
                        root.changePriceDuringSale == 'true' ? (root.changePriceDuringSale = true) : (root.changePriceDuringSale = false);
                        root.giveDiscountDuringSale = localStorage.getItem('giveDicountDuringSale');
                        root.giveDiscountDuringSale == 'true' ? (root.giveDiscountDuringSale = true) : (root.giveDiscountDuringSale = false);
                        root.GetDenominationSetupData();
                        root.CounterDetail();
                    } else {
                        this.isDayAlreadyStart = false;
                    }
                }
            }
            this.GetProductList();

        },

    }

</script>

<style scoped>
    @media (max-width: 1180px) {
        .customButton {
            padding: 3px !important;
        }

        .RemovePadding {
            padding: 0px !important;
        }
    }

    @media (max-width: 700px) {
        .customButton {
            padding: 3px !important;
        }

        .RemovePadding {
            padding: 0px !important;
        }

        .displayHidden {
            display: none !important;
        }

        .btn-lg {
            padding: 0.25rem 0.5rem;
            font-size: 0.75rem;
            line-height: 1.5;
            border-radius: 0.2rem;
        }
    }
</style>
