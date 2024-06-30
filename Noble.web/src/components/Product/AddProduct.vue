<template>
    <div class="row" v-if="isValid('CanAddItem') || isValid('CanEditItem')">
        <div class="col-lg-12 px-3">
            <div class="row">
                <div class="col-sm-6">
                    <div class="col d-flex align-items-baseline">
                        <div class="media">
                            <span class="circle-singleline" style="background-color: #1761FD !important;">PR</span>
                            <div class="media-body align-self-center ms-3">
                                <h6 class="m-0 font-20">{{ $t('AddProduct.AddProduct') }} </h6>
                                <div class="col d-flex ">
                                    <p class="text-muted mb-0" style="font-size:13px !important;">{{ product.code }}</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6" style="margin-top:25px;" v-if="allowBranches">
                    <branch-dropdown v-model="product.branchesIdList" :modelValue="product.branchesIdList" :ismultiple="true" />
                </div>
            </div>

            <hr class="hr-dashed hr-menu mt-0" />
            <h5 class="fw-bold">Item Information</h5>

            <div class="row">
                <div class="col-lg-8">
                    <div class="row form-group">
                        <label class="col-form-label col-lg-3">
                            <span id="ember695" class="tooltip-container text-dashed-underline "> {{
                                $t('AddProduct.ProductCode') }} </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <div class="row">
                                <div class="col-lg-4">
                                    <input v-model="product.code" disabled class="form-control" v-bind:key="rendered"
                                        type="text">
                                </div>
                                <div class="col-lg-6">
                                    <div class="checkbox form-check-inline mx-2 mt-2" :key="render + 'add'">
                                        <input type="checkbox" id="inlineCheckbox8" v-model="product.serviceItem">
                                        <label for="inlineCheckbox8">{{ $t('AddProduct.ServiceItem') }}</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-3">
                            <span id="ember695" class="tooltip-container text-dashed-underline">{{ $t('AddProduct.ItemName')
                            }} : <span class="text-danger"> *</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <div class="row">
                                <div class="col-lg-6 form-group" v-if="english == 'true'">
                                    <input v-model="product.englishName" @update:modelValue="DisplayName()"
                                        :placeholder="$t('AddProduct.ItemName') " class="form-control"
                                        type="text">
                                </div>
                                <div class="col-lg-6 form-group" v-if="isOtherLang()">
                                    <input v-model="product.arabicName" @update:modelValue="DisplayName()"
                                        :placeholder="$t('AddProduct.ItemName') " class="form-control"
                                        type="text">
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-3 "> {{ $t('AddProduct.Description') }} </label>
                        <div class="col-lg-6">
                            <textarea class="form-control" @update:modelValue="DisplayName()" v-model="product.description"></textarea>
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-form-label col-lg-3 "> {{ $t('AddProduct.ProductCategory') }} : <span
                                class="text-danger">*</span> </label>
                        <div class="col-lg-6">
                            <categorydropdown  v-model="product.categoryId"
                                v-bind:modelValue="product.categoryId"  @update:modelValue="getSubcategory(product.categoryId)" />
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="card" style="height: 200px;padding:15px" :key="renderedImage">
                        <AddProductImage v-bind:disable="false" :imagePath="product.image" v-on:picPath="getImage"
                            v-bind:path="image" />
                    </div>

                    <div class="row">
                        <div class="form-group col-md-6">
                            <div class="checkbox form-check-inline mx-2" :key="render + 'add'">
                                <input type="checkbox" id="inlineCheckbox2" v-model="product.isActive">
                                <label for="inlineCheckbox2">{{ $t('AddProduct.Active') }} </label>
                            </div>
                        </div>
                        <div class="form-group col-md-6" v-if="isRaw == 'true'">
                            <div class="checkbox form-check-inline mx-2" :key="render + 'add'">
                                <input type="checkbox" id="inlineCheckbox7" v-model="product.isRaw">
                                <label for="inlineCheckbox7">{{ $t('AddProduct.RawProduct') }} </label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr class="hr-dashed hr-menu my-2" />

            <div class="row mb-5 pb-5" v-if="!product.serviceItem">
                <div class="accordion" id="accordionExample">
                    <div class="accordion-item">
                        <h5 class="accordion-header m-0" id="headingOne">
                            <button class="accordion-button fw-semibold collapsed" type="button" data-bs-toggle="collapse"
                                data-bs-target="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                                <b>Tax & Pricing</b>
                            </button>
                        </h5>
                        <div id="collapseOne" class="accordion-collapse collapse" aria-labelledby="headingOne"
                            data-bs-parent="#accordionExample" style="">
                            <div class="accordion-body">
                                <div class="row" v-bind:class="product.serviceItem ? 'mb-5' : ''">
                                    <div class="col-lg-8">
                                        <div class="row form-group">
                                            <label class="col-form-label col-lg-3 "> {{ $t('AddProduct.TaxMethod') }}
                                            </label>
                                            <div class="col-lg-6">
                                                <multiselect :options="['Inclusive', 'Exclusive']"
                                                    v-model="product.taxMethod" :show-labels="false"
                                                    placeholder="Select Type">
                                                </multiselect>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <label class="col-form-label col-lg-3 "> {{ $t('AddProduct.TaxRate') }} </label>
                                            <div class="col-lg-6">
                                                <taxratedropdown v-model="product.taxRateId"
                                                    v-bind:modelValue="product.taxRateId" />
                                            </div>
                                        </div>

                                        <div class="row form-group" v-if="isMultiUnit == 'true'">
                                            <label class="col-form-label col-lg-3 "> {{ $t('AddProduct.SalePriceUnit') }}
                                            </label>
                                            <div class="col-lg-6">
                                                <unitleveldropdown v-model="product.salePriceUnit"
                                                    v-bind:modelValue="product.salePriceUnit" />
                                            </div>
                                        </div>
                                        <div class="row form-group" v-if="wholesalePriceActivation">
                                            <label class="col-form-label col-lg-3 "> {{ $t('AddProduct.RetailPrice') }}
                                            </label>
                                            <div class="col-lg-6">
                                                <my-currency-input v-model="product.salePrice"></my-currency-input>
                                            </div>
                                        </div>
                                        <div class="row form-group" v-if="wholesalePriceActivation">
                                            <label class="col-form-label col-lg-3 "> {{ $t('AddProduct.WholesalePrice') }}
                                            </label>
                                            <div class="col-lg-6">
                                                <my-currency-input v-model="product.wholesalePrice"></my-currency-input>
                                            </div>
                                        </div>
                                        <div class="row form-group" v-else>
                                            <label class="col-form-label col-lg-3 "> {{ $t('AddProduct.salePrice') }}
                                            </label>
                                            <div class="col-lg-6">
                                                <my-currency-input v-model="product.salePrice"></my-currency-input>
                                            </div>
                                        </div>
                                        <div v-if="!product.serviceItem" class="row form-group">
                                            <label class="col-form-label col-lg-3 "> {{ $t('AddProduct.PurchasePrice') }}
                                            </label>
                                            <div class="col-lg-6">
                                                <my-currency-input v-model="product.purchasePrice"></my-currency-input>
                                            </div>
                                        </div>
                                        <div v-if="!product.serviceItem" class="row form-group">
                                            <label class="col-form-label col-lg-3 "> {{ $t('AddProduct.MinStockLevel') }}
                                            </label>
                                            <div class="col-lg-6">
                                                <input class="form-control " type="number" v-model="product.stockLevel" />
                                            </div>
                                        </div>
                                        <div v-if="!product.serviceItem" class="row form-group">
                                            <label class="col-form-label col-lg-3 "> Cost Type </label>
                                            <div class="col-lg-3">
                                                <div class="input-group">
                                                    <decimal-to-fixed @update:modelValue="OnInputCost"
                                                        v-model="product.costValue" />
                                                    <button v-on:click="OnChangType" class="btn btn-primary" type="button"
                                                        id="button-addon2">
                                                        {{ product.costSign }}
                                                    </button>
                                                </div>
                                            </div>
                                            <div class="col-lg-3">
                                                <input class="form-control" disabled type="text"
                                                    v-model="product.costPrice" />
                                            </div>
                                        </div>
                                    </div>
                                    <div v-if="!product.serviceItem" class="col-lg-4">
                                        <div class="row">
                                            <div class="form-group col-md-6" :key="render + 'e'">
                                                <div class="checkbox form-check-inline mx-2" :key="render + 'add'">
                                                    <input type="checkbox" id="inlineCheckbox3" v-model="product.isExpire">
                                                    <label for="inlineCheckbox3">{{ $t('AddProduct.ExpiryDate') }} </label>
                                                </div>
                                            </div>
                                            <div class="form-group col-md-6" :key="render + 'e'">
                                                <div class="checkbox form-check-inline mx-2" :key="render + 'add'">
                                                    <input type="checkbox" id="inlineCheckbox9" v-model="product.guarantee">
                                                    <label for="inlineCheckbox9">Guarantee </label>
                                                </div>
                                            </div>
                                            <div class="form-group col-md-6" v-if="isSerial">
                                                <div class="checkbox form-check-inline mx-2" :key="render + 'add'">
                                                    <input type="checkbox" id="inlineCheckbox5" v-model="product.serial">
                                                    <label for="inlineCheckbox5">{{ $t('AddProduct.Serial') }} </label>
                                                </div>
                                            </div>
                                            <!--<div class="form-group col-md-6">
                            <div class="checkbox form-check-inline mx-2" :key="render + 'add'">
                                <input type="checkbox" id="inlineCheckbox6" v-model="product.highUnitPrice">
                                <label for="inlineCheckbox6">{{ $t('AddProduct.HighUnitPrice') }} </label>
                            </div>
                        </div>-->

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="accordion-item">
                        <h5 class="accordion-header m-0" id="headingTwo">
                            <button class="accordion-button fw-semibold collapsed" type="button" data-bs-toggle="collapse"
                                data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                <b>Item Linking</b>
                            </button>
                        </h5>
                        <div id="collapseTwo" class="accordion-collapse collapse" aria-labelledby="headingTwo"
                            data-bs-parent="#accordionExample" style="">
                            <div class="accordion-body">
                                <div class="row" v-if="!product.serviceItem">
                                    <div class="col-lg-8">
                                        <div class="row form-group">
                                            <label class="col-form-label col-lg-3 ">{{ $t('AddProduct.Product') }}</label>
                                            <div class="col-lg-6">
                                                <productMasterdropdown v-model="product.productMasterId"
                                                    :modelValue="product.productMasterId" />
                                            </div>
                                        </div>

                                        <div class="row form-group">
                                            <label class="col-form-label col-lg-3 "> {{ $t('AddProduct.ProductSubcategory')
                                            }} </label>
                                            <div class="col-lg-6">
                                                <multiselect v-model="subCategoryId" @update:modelValue="OnSelectedValue(subCategoryId.id)"
                                                            :options="subCategoryOptions"
                                                            :disabled="product.categoryId == '' || product.categoryId == null || product.serviceItem"
                                                            :show-labels="false" track-by="name" label="name"
                                                            
                                                            v-bind:placeholder="$t('AddProduct.PleaseSelectSubCategory')">

                                                    <template v-slot:noResult>
                                                        <span class="btn btn-primary "
                                                            v-on:click="AddSubCategory('Add')">
                                                            {{ $t('AddProduct.AddSubCategory')}}
                                                        </span>
                                                        <br />
                                                    </template>

                                                </multiselect>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <label class="col-form-label col-lg-3 "> {{ $t('AddProduct.ProductBrand') }}
                                            </label>
                                            <div class="col-lg-6">
                                                <branddropdown v-model="product.brandId" :disabled="product.serviceItem"
                                                    :modelValue="product.brandId"  />
                                            </div>
                                        </div>

                                        <div class="row form-group">
                                            <label class="col-form-label col-lg-3 "> {{ $t('ProductGroup.ProductGroup') }}
                                            </label>
                                            <div class="col-lg-6">
                                                <productgroupdropdown v-model="product.productGroupId"
                                                    v-bind:modelValue="product.productGroupId" />
                                            </div>
                                        </div>

                                        <div class="row form-group">
                                            <label class="col-form-label col-lg-3 "> {{ $t('AddProduct.Shelf/Location') }}
                                            </label>
                                            <div class="col-lg-3">
                                                <input class="form-control " type="text" v-model="product.shelf"
                                                    :placeholder="$t('AddProduct.Shelf/Location')" />
                                            </div>
                                            <div class="col-lg-3">
                                                <input class="form-control " type="text" v-model="product.hsCode"
                                                    :placeholder="$t('AddProduct.HsCode')" />
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <label class="col-form-label col-lg-3 "> {{ $t('AddProduct.Barcode') }} </label>
                                            <div class="col-lg-3">
                                                <input class="form-control " @update:modelValue="BarCodeLength(product.barcode)"
                                                    type="text" v-model="product.barcode" />
                                            </div>
                                            <div class="col-lg-3">
                                                <button v-if="product.barcode == '' || product.barcode == null"
                                                    class="btn btn-outline-primary form-group"
                                                    v-on:click="generateBarcode(false)">
                                                    {{
                                                        $t('AddProduct.Generate')
                                                    }}
                                                </button>
                                                <button v-if="product.barcode != '' && product.barcode != null"
                                                    class="btn btn-outline-danger form-group"
                                                    v-on:click="generateBarcode(true)">
                                                    {{
                                                        $t('AddProduct.Delete')
                                                    }}
                                                </button>
                                            </div>
                                            <div v-if="product.barcode != '' && product.barcode != null" class="col-lg-3">
                                                <barcode :height="30" v-bind:value="product.barcode"></barcode>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="accordion-item">
                        <h5 class="accordion-header m-0" id="headingThree">
                            <button class="accordion-button fw-semibold collapsed" type="button" data-bs-toggle="collapse"
                                data-bs-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                                <b>{{ $t('AddProduct.ItemsAttribute') }}</b>
                            </button>
                        </h5>
                        <div id="collapseThree" class="accordion-collapse collapse" aria-labelledby="headingThree"
                            data-bs-parent="#accordionExample" style="">
                            <div class="accordion-body">
                                <div v-if="!product.serviceItem" class="row">
                                    <div class="col-md-4 col-sm-12 form-group">
                                        <label>
                                            {{ $t('AddProduct.PackSize') }} :
                                        </label>
                                        <div class="d-flex">
                                            <input style="width:40%" class="form-control " type="number"
                                                v-model="product.length" />
                                            <span style="padding-top:7px; width:20%;text-align: center;">x</span>
                                            <input style="width:40%" class="form-control " type="number"
                                                v-model="product.width" />
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-12 form-group" v-if="colorVariants">
                                        <label>{{ $t('AddProduct.Color') }} : </label>
                                        <color-multiselect-dropdown v-model="colorIds" :disabled="product.serviceItem"
                                            v-bind:modelValue="colorIds" />
                                    </div>
                                    <div class="col-md-4 col-sm-12 form-group" v-else>
                                        <label>{{ $t('AddProduct.Color') }} : </label>
                                        <colordropdown v-model="product.colorId" :disabled="product.serviceItem"
                                            v-bind:modelValue="product.colorId" />
                                    </div>

                                    <div class="col-md-4 col-sm-12 form-group">
                                        <label>{{ $t('AddProduct.Assortment') }} : </label>
                                        <input class="form-control " type="text" v-model="product.assortment" />
                                    </div>

                                    <div class="col-md-12">
                                        <hr />
                                    </div>
                                    <div class="col-md-4 col-sm-12 form-group">
                                        <label>{{ $t('AddProduct.StyleNumber') }} : </label>
                                        <input class="form-control " type="text" v-model="product.styleNumber" />
                                    </div>

                                    <div class="col-md-4 col-sm-12 form-group">
                                        <label> {{ $t('AddProduct.Origin') }} : </label>
                                        <div>
                                            <origindropdown v-model="product.originId" :disabled="product.serviceItem"
                                             v-bind:modelValue="product.originId">
                                            </origindropdown>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-12 form-group">
                                        <div class="checkbox form-check-inline mx-2" :key="render + 'add'">
                                            <input type="checkbox" id="inlineCheckbox1"
                                                v-on:change="changeValue(isSaleReturn)" v-model="isSaleReturn">
                                            <label for="inlineCheckbox1" class="mb-0">{{ $t('AddProduct.IsSaleReturnDays')
                                            }} </label>
                                        </div>
                                        <input class="form-control " v-if="isSaleReturn"
                                            type="number" v-model="product.saleReturnDays" />
                                    </div>

                                    <div class="col-md-12">
                                        <hr />
                                    </div>

                                    <div class="col-md-4 col-sm-12 form-group" v-if="colorVariants">
                                        <label>{{ $t('AddProduct.Size') }} : </label>
                                        <size-multiselect-dropdown v-model="sizeIds" :disabled="product.serviceItem"
                                            v-bind:modelValue="sizeIds" />
                                    </div>
                                    <div class="col-md-4 col-sm-12 form-group" v-if="isMultiUnit == 'true'">
                                        <label>High Unit : </label>
                                        <div>
                                            <unitleveldropdown v-model="product.levelOneUnit"
                                                v-bind:modelValue="product.levelOneUnit">
                                            </unitleveldropdown>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-12 form-group" v-else>
                                        <label>{{ $t('AddProduct.Unit') }} :</label>
                                        <div>
                                            <unitdropdown v-model="product.unitId" :disabled="product.serviceItem"
                                                 v-bind:modelValue="product.unitId">
                                            </unitdropdown>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-12 form-group" v-if="isMultiUnit == 'true'">
                                        <label>{{ $t('AddProduct.UnitPerPack') }}: </label>
                                        <input class="form-control" v-model="product.unitPerPack"
                                            @update:modelValue="unitPackSizeChange(product.unitPerPack)" type="number" />
                                    </div>
                                    <div class="col-md-4 col-sm-12 form-group" v-if="isMultiUnit == 'true'">
                                        <label>Low Unit:</label>
                                        <div>
                                            <unitdropdown v-model="product.unitId" :disabled="product.serviceItem"
                                                 v-bind:modelValue="product.unitId">
                                            </unitdropdown>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-12 form-group" v-else>
                                        <label>{{ $t('AddProduct.Size') }} : </label>
                                        <sizedropdown v-model="product.sizeId" :disabled="product.serviceItem"
                                            v-bind:modelValue="product.sizeId" />
                                    </div>
                                    <div class="col-md-12">
                                        <hr />
                                    </div>
                                    <div class="col-md-4 col-sm-12 form-group">
                                        <label>{{ $t('AddProduct.Scheme') }} :</label>
                                        <div class="d-flex">
                                            <input style="width:40%" class="form-control " type="number"
                                                v-model="product.schemeQuantity" />
                                            <span style="padding-top:7px; width:20%;text-align: center;">+</span>
                                            <input style="width:40%" class="form-control" type="number"
                                                v-model="product.scheme" />
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-12 form-group">
                                        <label>{{ $t('AddProduct.MinimumWholesaleQuantity') }} : </label>
                                        <input class="form-control " type="text" v-model="product.wholesaleQuantity" />
                                    </div>
                                    <div class="col-md-12">
                                        <hr />
                                    </div>
                                    <div class="col-md-4 col-sm-12 form-group">
                                        <label>{{ $t('SKU') }} : </label>
                                        <input class="form-control " type="text" v-model="product.sku" />
                                    </div>
                                    <div class="col-md-4 col-sm-12 form-group">
                                        <label>{{ $t('Part Number') }} : </label>
                                        <input class="form-control " type="text" v-model="product.partNumber" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="accordion-item">
                        <h5 class="accordion-header m-0" id="headingFour">
                            <button class="accordion-button fw-semibold collapsed" type="button" data-bs-toggle="collapse"
                                data-bs-target="#collapseFour" aria-expanded="false" aria-controls="collapseFour">
                                <b>Item Display Settings</b>
                            </button>
                        </h5>
                        <div id="collapseFour" class="accordion-collapse collapse" aria-labelledby="headingFour"
                            data-bs-parent="#accordionExample" style="">
                            <div class="accordion-body">
                                <div class="row">
                                    <div class="col-md-6 col-sm-12 form-group">
                                        <label class="col-form-label">Item Display Name : <span class="text-danger">*</span> </label>
                                        <div>
                                            <multiselect :options="combinations" v-model="product.displayName" @update:modelValue="getOptionsfromElements"
                                                @open="getOptionsfromElements" :show-labels="false" placeholder="Select Type">
                                            </multiselect>

                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-12 form-group">
                                        <label class="col-form-label">Item Display Name For Print : <span class="text-danger">*</span> </label>
                                        <div>
                                            <multiselect :options="combinations" v-model="product.displayNameForPrint"
                                                @update:modelValue="getPrintOptionsfromElements" @open="getPrintOptionsfromElements" :show-labels="false"
                                                placeholder="Select Type">
                                            </multiselect>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="accordion-item">
                        <h5 class="accordion-header m-0" id="headingFive">
                            <button class="accordion-button fw-semibold collapsed" type="button" data-bs-toggle="collapse"
                                data-bs-target="#collapseFive" aria-expanded="false" aria-controls="collapseFive">
                                <b>Multi Pricing</b>
                            </button>
                        </h5>
                        <div id="collapseFive" class="accordion-collapse collapse" aria-labelledby="headingFive"
                            data-bs-parent="#accordionExample" style="">
                            <div class="accordion-body">
                                <div v-if="!product.serviceItem" class="row mb-5">
                                    <div class="col-lg-12">
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th class="text-center" style="width:30%;">Price Type</th>
                                                    <th class="text-center" style="width:30%;">Price</th>
                                                    <th class="text-center" style="width:30%;">Status</th>
                                                    <th class="text-center" style="width:10%;"></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(person, index) in product.priceRecords" :key="index">
                                                    <td class="border-top-0 text-center">
                                                        <priceLabelingDropdown v-model="person.priceLabelingId" :isSingle="true"
                                                            v-bind:modelValue="person.priceLabelingId" />
                                                    </td>
                                                    <td class="border-top-0 text-center">
                                                        <decimal-to-fixed v-model="person.newPrice" />
                                                    </td>
                                                    <td class="border-top-0 text-center">
                                                        <input type="checkbox" id="inlineCheckbox1" v-model="person.isActive" />
                                                    </td>

                                                    <td class="border-top-0 pt-0 text-end">
                                                        <button title="Remove Item" id="bElim" type="button"
                                                            class="btn btn-sm btn-soft-danger btn-circle" v-on:click="RemoveRow(index)">
                                                            <i class="dripicons-trash" aria-hidden="true"></i>
                                                        </button>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="7" class="border-top-0 text-end">
                                                        <button id="but_add" class="btn btn-success btn-sm" v-on:click="AddRow()">
                                                            Add Price Rcord
                                                        </button>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div v-if="!loading" class=" col-lg-12 invoice-btn-fixed-bottom">
            <div class="row">
                <div v-if="!loading" class="col-md-12">
                    <div class="button-items"
                        v-if="product.id == '00000000-0000-0000-0000-000000000000' && isValid('CanAddItem')">
                        <button class="btn btn-outline-primary" v-bind:disabled="v$.product.$invalid"
                            v-on:click="SaveProduct"><i class="far fa-save "></i> {{ $t('AddCustomer.btnSave') }}</button>
                        <button class="btn btn-danger" v-on:click="GoToProduct">{{ $t('AddCustomer.Cancel') }}</button>
                    </div>
                    <div class="button-items"
                        v-if="product.id != '00000000-0000-0000-0000-000000000000' && isValid('CanEditItem')">
                        <button class="btn btn-outline-primary" v-bind:disabled="v$.product.$invalid"
                            v-on:click="SaveProduct"><i class="far fa-save "></i> {{ $t('AddCustomer.btnUpdate') }}</button>
                        <button class="btn btn-danger" v-on:click="GoToProduct">{{ $t('AddCustomer.Cancel') }}</button>
                    </div>
                </div>
            </div>
        </div>
        <subcategorymodal :show="show" :subCategory="newSubCategory" v-if="show" :categoryid="product.categoryId"
            @close="IsSave" :type="type" />

        <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
    </div>

    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>

<script>
 import clickMixin from '@/Mixins/clickMixin'
    import {
        maxLength,
        requiredIf
    } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Multiselect from 'vue-multiselect'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

    import Vue3Barcode from 'vue3-barcode'
export default {
    mixins: [clickMixin],

    components: {
        'barcode': Vue3Barcode,
        Multiselect,
        Loading
    },
    setup() {
            return { v$: useVuelidate() }
        },
    data: function () {

        return {
            salutatioRender: 0,
            reender: 0,
            selectAllBrnaches: false,
            allowBranches: false,
            arabic: '',
            english: '',
            colorVariants: false,
            isSerial: false,
            isRaw: '',
            isMultiUnit: '',
            subCategoryOptions: [],
            value: '',
            roles: [],
            options: ['Wholesaler', 'Retailer', 'Dealer', 'Distributer'],
            active: 'personal',
            rendered: 0,
            storedColors: [],
            renderedImage: 0,
            language: 'Nothing',
            isDelete: false,
            isBarCodeGenerated: false,
            elements: [],
            combinations: [],
            product: {
                id: '00000000-0000-0000-0000-000000000000',
                image: '',
                costSign: 'F',
                costValue: 0,
                costPrice: 0,
                productMasterId: '',
                priceRecords: [],
                englishName: '',
                arabicName: '',
                displayName: '',
                displayNameForPrint: '',
                categoryId: '',
                brandId: '',
                barcode: '',
                length: 1,
                width: 1,
                subCategoryId: '',
                imagePath: '',
                isActive: true,
                isRaw: false,
                serial: false,
                guarantee: false,
                levelOneUnit: '',
                basicUnit: '',
                unitPerPack: null,
                salePriceUnit: '',
                salePrice: 0,
                purchasePrice: 0,
                unitId: '',
                sizeIdList: [],
                colorIdList: [],
                assortment: '',
                hsCode: '',
                styleNumber: '',
                taxMethod: '',
                serviceItem: false,
                wholesalePrice: 0,
                highUnitPrice: false,
                wholesaleQuantity: '',
                schemeQuantity: '',
                scheme: '',
                productGroupId: '',
                branchesIdList: [],
                partNumber: '',
                sku: ''
            },
            sizeIds: '',
            colorIds: '',
            loading: false,
            catId: '',
            isCounter: 0,
            isEnabled: false,
            isDisable: false,
            isSaleReturn: false,
            isMasterProduct: false,
            randomNumber: '',
            render: 0,
            subCategoryId: [],
            decoded: [],
            permission: [],
            image: '',

            show: false,
            type: '',
            productId: {
                id: '',
                name: '',
                nameArabic: '',
            },
            newSubCategory: {
                id: '',
                code: '',
                name: '',
                nameArabic: '',
                description: '',
                categoryId: '',
                isActive: true
            },
            wholesalePriceActivation: false,
            itemViewSetupList: [],
            itemViewSetupListForPrint: [],
            itemListViewSetup: [],

            productMasterOptions: [],
            productCategoryOptions: [],
            productBrandOptions: [],
            productColorOptions: [],
            productOriginOptions: [],
            productUnitOptions: [],
            productSizeOptions: [],
            renderOpt: 0
        }
    },

    filters: {},
    validations() {
        return {
            product: 
            {
                englishName: 
                {
                    maxLength: maxLength(250)
                },
                arabicName: 
                {
                    required: requiredIf(function () {
                        return !this.product.englishName;
                    }),
                    maxLength: maxLength(250)
                },
                categoryId: 
                {
                    required: requiredIf(function () {
                        return this.product.serviceItem;
                    }),
                },
                levelOneUnit: {},
                unitId: 
                {
                    required: requiredIf(function () {
                        return this.product.levelOneUnit;
                    }),
                },
                unitPerPack: 
                {
                    required: requiredIf(function () {
                        return this.product.levelOneUnit;
                    }),
                },
            }
        }
    },
    methods: {
        getPrintOptionsfromElements: function () {
            this.pushElementsForPrintDisplayName();
            this.generateCombinations();
            this.renderOpt++;
        },
        getOptionsfromElements: function () {
            this.pushElementsForDisplayName();
            this.generateCombinations();
            this.renderOpt++;
        },
        pushElementsForPrintDisplayName: function () {
            var root = this;
            this.elements = [];
            root.itemViewSetupListForPrint.forEach(x => {

                if (x.name == 'Item Code') {
                    root.elements.push(root.product.code);
                }
                else if (x.name == 'Item Name (English)') {
                    if (root.product.englishName != "" && root.product.englishName != null && root.product.englishName != undefined) {
                        root.elements.push(root.product.englishName);
                    }
                }
                else if (x.name == 'Item Name (Arabic)') {
                    if (root.product.arabicName != "" && root.product.arabicName != null && root.product.arabicName != undefined) {
                        root.elements.push(root.product.arabicName);
                    }
                }
                else if (x.name == 'Item Description') {
                    if (root.product.description != "" && root.product.description != null && root.product.arabicName != undefined) {
                        root.elements.push(root.product.description);
                    }
                }
                else if (x.name == 'Item Category') {
                    if (root.product.categoryId != "" && root.product.categoryId != null && root.product.categoryId != undefined) {
                        var category = root.productCategoryOptions.find(x => x.id == root.product.categoryId);
                        root.elements.push(category.name);
                    }
                }
                else if (x.name == 'Item Barcode') {
                    if (root.product.barcode != "" && root.product.barcode != null && root.product.barcode != undefined) {
                        root.elements.push(root.product.barcode);
                    }
                }
                else if (x.name == 'Item Style/Model') {
                    if (root.product.styleNumber != "" && root.product.styleNumber != null && root.product.styleNumber != undefined) {
                        root.elements.push(root.product.styleNumber);
                    }
                }
                else if (x.name == "Item Brand") {
                    
                    if (root.product.brandId != "" && root.product.brandId != null  && root.product.brandId != undefined) {
                        var brand = root.productBrandOptions.find(x => x.id == root.product.brandId);
                        root.elements.push(brand.name);
                    }
                }
                else if (x.name == "Item Origin") {
                    if (root.product.originId != "" && root.product.originId != null && root.product.originId != undefined) {
                        var origin = root.productOriginOptions.find(x => x.id == root.product.originId);
                        root.elements.push(origin.name);
                    }
                }
                else if (x.name == "Item Size") {
                    if (root.product.sizeId != "" && root.product.sizeId != null && root.product.sizeId != undefined) {
                        var size = root.productSizeOptions.find(x => x.id == root.product.sizeId);
                        root.elements.push(size.name);
                    }
                }
                else if (x.name == "Item Color") {
                    if (root.product.colorId != "" && root.product.colorId != null && root.product.colorId != undefined) {
                        var color = root.productColorOptions.find(x => x.id == root.product.colorId);
                        root.elements.push(color.name);
                    }
                }
                else if (x.name == "Item Unit") {
                    if (root.product.unitId != "" && root.product.unitId != null && root.product.unitId != undefined) {
                        var unit = root.productUnitOptions.find(x => x.id == root.product.unitId);
                        root.elements.push(unit.name);
                    }
                }
            });
        },
        pushElementsForDisplayName: function () {
            var root = this;
            this.elements = [];
            root.itemViewSetupList.forEach(x => {

                if (x.name == 'Item Code') {
                    root.elements.push(root.product.code);
                }
                else if (x.name == 'Item Name (English)') {
                    if (root.product.englishName != "" && root.product.englishName != null && root.product.englishName != undefined) {
                        root.elements.push(root.product.englishName);
                    }
                }
                else if (x.name == 'Item Name (Arabic)') {
                    if (root.product.arabicName != "" && root.product.arabicName != null && root.product.arabicName != undefined) {
                        root.elements.push(root.product.arabicName);
                    }
                }
                else if (x.name == 'Item Description') {
                    if (root.product.description != "" && root.product.description != null && root.product.description != undefined) {
                        root.elements.push(root.product.description);
                    }
                }
                else if (x.name == 'Item Category') {
                    if (root.product.categoryId != "" && root.product.categoryId != null && root.product.categoryId != undefined) {
                        var category = root.productCategoryOptions.find(x => x.id == root.product.categoryId);
                        root.elements.push(category.name);
                    }
                }
                else if (x.name == 'Item Barcode') {
                    if (root.product.barcode != "" && root.product.barcode != null && root.product.barcode != undefined) {
                        root.elements.push(root.product.barcode);
                    }
                }
                else if (x.name == 'Item Style/Model') {
                    if (root.product.styleNumber != "" && root.product.styleNumber != null && root.product.styleNumber != undefined) {
                        root.elements.push(root.product.styleNumber);
                    }
                }
                else if (x.name == "Item Brand") {
                   
                   var aa = root.product.brandId
                    if (root.product.brandId != "" && root.product.brandId != null && aa != undefined) {
                        var brand = root.productBrandOptions.find(x => x.id == root.product.brandId);
                        root.elements.push(brand.name);
                    }
                }
                else if (x.name == "Item Origin") {
                    if (root.product.originId != "" && root.product.originId != null && root.product.originId != undefined) {
                        var origin = root.productOriginOptions.find(x => x.id == root.product.originId);
                        root.elements.push(origin.name);
                    }
                }
                else if (x.name == "Item Size") {
                    if (root.product.sizeId != "" && root.product.sizeId != null && root.product.sizeId != undefined) {
                        var size = root.productSizeOptions.find(x => x.id == root.product.sizeId);
                        root.elements.push(size.name);
                    }
                }
                else if (x.name == "Item Color") {
                    if (root.product.colorId != "" && root.product.colorId != null && root.product.colorId != undefined) {
                        var color = root.productColorOptions.find(x => x.id == root.product.colorId);
                        root.elements.push(color.name);
                    }
                }
                else if (x.name == "Item Unit") {
                    if (root.product.unitId != "" && root.product.unitId != null && root.product.unitId != undefined) {
                        var unit = root.productUnitOptions.find(x => x.id == root.product.unitId);
                        root.elements.push(unit.name);
                    }
                }
            });
        },
        generateCombinations() {
            this.combinations = [];
            this._generateCombinations([], 0);
        },
        _generateCombinations(currentCombination, index) {
            if (index === this.elements.length) {
                if (currentCombination.length > 0) {
                    this.combinations.push(currentCombination.join(', '));
                }
                return;
            }

            // Include the current element in the combination
            this._generateCombinations([...currentCombination, this.elements[index]], index + 1);

            // Exclude the current element from the combination
            this._generateCombinations(currentCombination, index + 1);
        },
        GetItemViewSetup: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }


            root.$https.get('/Product/GetItemsViewSetup', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {

                    root.itemViewSetupList = [];
                    root.itemViewSetupListForPrint = [];

                    root.itemViewSetupList = response.data.itemViewSetupList;
                    root.itemViewSetupListForPrint = response.data.itemViewSetupListForPrint;
                    root.itemListViewSetup = response.data.itemListViewSetup;


                }
            });
        },
        DisplayName: function () {
            this.salutatioRender++;
        },

        RemoveRow: function (index) {
            this.product.priceRecords.splice(index, 1);
        },

        AddRow: function () {

            // if(this.product.priceRecords.length>0)
            // {
            //     var isFind=this.product.priceRecords.some(x=>x.priceLabelingId==)
            // }


            this.product.priceRecords.push({
                id: '',
                newPrice: 0,
                price: 0,
                isActive: false,
                priceLabelingId: '',

            });


        },

        OnInputCost() {

            if (this.product.costSign === '%') {
                if (this.product.costValue > 0 && this.product.purchasePrice > 0) {
                    const val = ((this.product.purchasePrice) / 100) * this.product.costValue;
                    this.product.costPrice = parseFloat(this.product.purchasePrice + val).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                } else {
                    this.product.costPrice = 0;
                }
            } else {
                if (this.product.costValue > 0) {
                    const val = (this.product.costValue);
                    this.product.costPrice = parseFloat(val).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");

                } else {
                    this.product.costPrice = 0;
                }

            }

        },
        OnChangType() {
            if (this.product.costSign === '%') {
                this.product.costSign = 'F';
                this.product.costValue = 0;
            } else {
                this.product.costSign = '%';
                this.product.costValue = 0;
            }
            this.OnInputCost();

        },
        onBarcodeScanned(barcode) {

            if (localStorage.getItem("BarcodeScan") != 'AddProduct')
                return
            this.product.barcode = barcode

        },
        BarCodeLength(barcode) {

            if (barcode.length > 20) {
                barcode = barcode.slice(0, -1);
                this.product.barcode = barcode
                this.$swal({
                    title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                    text: 'Bar Code Length not greater than 20',
                    type: 'error',
                    confirmButtonClass: "btn btn-danger",
                    icon: 'error',
                    timer: 4000,
                    timerProgressBar: true,
                });
            }

        },
        GetMasterProduct: function (x) {

            this.product.productMasterId = x.id;
            this.product.englishName = x.name;
            this.product.arabicName = x.nameArabic;
        },
        IsSave: function () {
            this.show = false;
            this.getSubcategory(this.product.categoryId);
        },
        AddSubCategory: function () {
            this.newSubCategory = {
                id: '00000000-0000-0000-0000-000000000000',
                code: '',
                name: '',
                nameArabic: '',
                description: '',
                categoryId: '',
                isActive: true
            }

            this.show = !this.show;
            this.type = "Add";
        },
        unitPackSizeChange: function (value) {
            this.product.width = value;
        },
        languageChange: function (lan) {
            if (this.language == lan) {
                if (this.product.id == '00000000-0000-0000-0000-000000000000') {

                    var getLocale = this.$i18n.locale;
                    this.language = getLocale;

                    this.$router.go('/addproduct');
                } else {
                    this.$swal({
                        title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You cannot Change language during update, otherwise your current page data will be lose!' : 'لا يمكنك تغيير اللغة أثناء التحديث ، وإلا ستفقد بيانات صفحتك الحالية!',
                        type: 'error',
                        confirmButtonClass: "btn btn-danger",
                        icon: 'error',
                        timer: 4000,
                        timerProgressBar: true,
                    });
                }
            }

        },

        focusOut: function () {

            return this.product.salePrice = this.product.salePrice.toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
        },

        changeValue: function (value) {
            if (value == false) {
                this.product.saleReturnDays = '';
            }
        },
        RanderImagePath: function (value) {
            if (value == true) {
                this.renderedImage++;

            }
        },
        getImage: function (value) {
            this.product.image = value;
            this.isDelete = true;
        },
        OnSelectedValue: function (id) {

            this.product.subCategoryId = id;
        },
        getSubcategory: function (event) {
            this.catId = event;
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            this.subCategoryId = [];
            this.subCategoryOptions = [];
            this.$https.get('/Product/GetSubCategoryInformation?categoryId=' + event + '&isActive=' + true, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }).then(function (response) {
                if (response.data != null) {
                    response.data.results.subCategories.forEach(function (rout) {
                        if (rout.id == root.product.subCategoryId) {
                            root.subCategoryId.push({
                                id: rout.id,
                                name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (rout.name != "" ? rout.code + ' ' + rout.name : rout.code + ' ' + rout.nameArabic) : (rout.nameArabic != "" ? rout.code + ' ' + rout.nameArabic : rout.code + ' ' + rout.name)
                            })
                        }
                        root.subCategoryOptions.push({
                            id: rout.id,
                            name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (rout.name != "" ? rout.code + ' ' + rout.name : rout.code + ' ' + rout.nameArabic) : (rout.nameArabic != "" ? rout.code + ' ' + rout.nameArabic : rout.code + ' ' + rout.name)
                        })
                    })
                }
            })
        },

        generateBarcode: function (x) {
            if (x) {
                this.randomNumber = 0; //multiply to generate random number between 0, 100
                this.product.barcode = '';
                this.isDisable = false;
                this.isBarCodeGenerated = false;
                this.isEnabled = false;
            } else {

                this.randomNumber = Math.floor(Math.random() * 10000000000); //multiply to generate random number between 0, 100
                this.product.barcode = this.randomNumber;
                this.isBarCodeGenerated = true;
                this.isDisable = true;
                this.isEnabled = true;
            }

        },
        writeBarcode: function () {
            this.isDisable = true
            this.isEnabled = true

        },
        GoToProduct: function () {
            if (this.isValid('CanViewItem')) {
                this.$router.push('/products');
            } else {
                this.$router.go();
            }

        },
        makeActive: function (tab) {

            this.active = tab;
        },

        getSizeId: function (value) {
            var sizeId = [];
            for (var i = 0; i < value.length; i++) {
                sizeId[i] = value[i].id
            }
            return sizeId;
        },

        getColorId: function (value) {
            var sizeId = [];
            for (var i = 0; i < value.length; i++) {
                sizeId[i] = value[i].id
            }
            return sizeId;
        },

        SaveProduct: function () {

            if (this.colorVariants) {
                if (this.sizeIds != null && this.sizeIds != undefined && this.sizeIds != "") {
                    this.product.sizeIdList = this.getSizeId(this.sizeIds);
                }
                if (this.colorIds != null && this.colorIds != undefined && this.colorIds != "") {
                    this.product.colorIdList = this.getColorId(this.colorIds);
                }
            }

            if (this.product.barcode != "" && this.product.barcode != null && this.product.barcode.length > 20) {
                for (var i = 0; i < this.product.barcode.length; i++) {
                    if (this.product.barcode.length < 20) {
                        break;
                    } else {
                        this.product.barcode = this.product.barcode.slice(0, -1);

                    }

                }

            }


            this.loading = true;
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            if (!this.isSaleReturn) {
                this.product.saleReturnDays = 0;
            }
            //if (this.isMultiUnit == 'true') {
            //    this.product.levelOneUnit = this.product.levelOneUnit.name;
            //    this.product.basicUnit = this.product.basicUnit.name;
            //    this.product.salePriceUnit = this.product.salePriceUnit.name;
            //}
            if (this.product.salePrice == null || this.product.salePrice == '') {
                this.product.salePrice = 0;
            }

            if (this.product.serviceItem && this.product.categoryId == '') {
                this.product.categoryId = '00000000-0000-0000-0000-000000000000';
            }

            this.$https.post('/Product/SaveProductInformation?isBarCodeGenerated=' + this.isBarCodeGenerated, this.product, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(response => {

                    if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Add") {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                            text: response.data.message.isAddUpdate,
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            buttonStyling: false,
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true,

                        }).then(function (ok) {
                            if (ok != null) {
                                if (root.isValid('CanViewItem')) {
                                    root.$router.push('/products');
                                } else {
                                    root.$router.go();
                                }
                            }
                        });
                    } else if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Update") {
                        root.info = response.data.bpi
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                            text: response.data.message.isAddUpdate,
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            buttonStyling: false,
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true,

                        }).then(function (ok) {
                            if (ok != null) {
                                if (root.isValid('CanViewItem')) {
                                    root.$router.push('/products');
                                } else {
                                    root.$router.go();
                                }
                            }
                        });
                    } else {
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
                .catch(error => {
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
                })
                .finally(() => root.loading = false)
        },
        AutoIncrementCode: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https
                .get('/Product/ProductAutoGenerateCode', {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {
                    if (response.data != null) {
                        root.product.code = response.data;

                        root.rendered++
                    }
                });
        },
        getBase64Image: function (path) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.$https
                .get('/Contact/GetBaseImage?filePath=' + path, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {
                    if (response.data != null) {
                        root.image = response.data;
                        root.renderedImage++
                        root.isDelete = true;
                    }
                });
        },
        GetProductMasterData: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('Product/ProductMasterList?isActive=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.productMasterOptions = response.data.results.productMasters;
                }
            });
        },
        GetCategoryData: function () {
            var root = this;
            var url = '/Product/GetCategoryInformation?isActive=true';
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.productCategoryOptions = response.data.results.categories;
                }
            });

        },
        GetBrandData: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('Product/BrandList?isActive=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.productBrandOptions = response.data.results.brands;
                }
            });
        },
        GetOriginData: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('Product/OriginList?isActive=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {

                    root.productOriginOptions = response.data.results.origins;
                }
            });
        },
        GetSizeData: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('Product/SizeList?isActive=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.productSizeOptions = response.data.results.sizes;
                }
            });
        },
        GetColorData: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('Product/ColorList?isActive=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.productColorOptions = response.data.results.colors;
                }
            });
        },
        GetUnitData: function () {
                var root = this;
                var token = '';

                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Product/UnitList?isActive=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.productUnitOptions = response.data.results.units;
                    }
                });
            },
        allProductFilters: function()
        {
            this.GetProductMasterData();
            this.GetCategoryData();
            this.GetBrandData();
            this.GetOriginData();
            this.GetSizeData();
            this.GetColorData();
            this.GetUnitData();
        }
    },

    created() {
        var root =  this;

            if(root.$route.query.Add == 'false')
            {
            root.$route.query.data = root.$store.isGetEdit;
            }

        this.GetItemViewSetup();
        this.$emit('update:modelValue', this.$route.name);
        this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;

        var barcode = '';
        var interval;
        document.addEventListener('keydown', function (evt) {
            if (interval)
                clearInterval(interval);
            if (evt.code === 'Enter') {
                if (barcode) {
                    root.onBarcodeScanned(barcode);
                }
                barcode = '';
                return;

            }
            if (evt.key !== 'Shift')
                barcode += evt.key;
        });
        //End
        localStorage.setItem("BarcodeScan", 'AddProduct');

        this.isMultiUnit = localStorage.getItem('IsMultiUnit');
        this.isRaw = localStorage.getItem('IsProduction');
        this.isMasterProduct = localStorage.getItem('IsMasterProductPermission') == 'true' ? true : false;
        this.wholesalePriceActivation = localStorage.getItem('WholeSalePriceActivation') == 'true' ? true : false;

        
        if (this.$route.query.data == undefined) {
            this.AutoIncrementCode();
            //    this.GetLastDetails();
        }

        if (this.$route.query.data != undefined) {

            this.product = this.$route.query.data;
            this.sizeIds = this.product.sizeIdList;
            this.colorIds = this.product.colorIdList;

            this.render++;
            this.isDisable = true;
            this.isEnabled = true;
            this.getSubcategory(this.product.categoryId);
            if (this.product.saleReturnDays != null && this.product.saleReturnDays != "" && this.product.saleReturnDays != 0) {
                this.isSaleReturn = true;
            }

            if (this.product.image != "" && this.product.image != null && this.product.image != undefined) {
                this.getBase64Image(this.product.image);
            }
        } else {
            this.product.taxMethod = localStorage.getItem('taxMethod');
            this.product.taxRateId = localStorage.getItem('TaxRateId');
        }
    },

    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.isSerial = localStorage.getItem('IsSerial') == 'true' ? true : false;
        this.colorVariants = localStorage.getItem('ColorVariants') == 'true' ? true : false;
        var getLocale = this.$i18n.locale;
        this.language = getLocale;
        this.allProductFilters();
    },
}
</script>

<style>
.circle-singleline {
    margin: 20px;
    width: 60px;
    height: 60px;
    border-radius: 50%;
    font-size: 30px;
    text-align: center;
    background: blue;
    color: #fff;
    vertical-align: middle;
    line-height: 60px;
}</style>