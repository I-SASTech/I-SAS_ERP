<template>
  <div class="row">
    <div class="col-md-12 ml-auto mr-auto">
      <div class="row">
        <div class="col-sm-12">
          <div class="page-title-box">
            <div class="row">
              <div class="col">
                <h4 class="page-title">
                  {{ $t("Add Items BarCode") }}
                </h4>
              </div>
              <div class="col-auto align-self-center">
                <a class="btn btn-sm btn-outline-primary mx-1" v-on:click="DowmloadCSV()">
                  Download Template
                  <i class="fas fa-file-pdf float-right"></i>
                </a>
                <a v-if="isValid('CanImportProduct')" v-on:click="ImportDataFromCsv"
                   href="javascript:void(0);" class="btn btn-sm btn-outline-primary me-2">
                    <i class="align-self-center icon-xs ti-plus"></i>
                    {{$t('Product.ImportItems')}}
                </a>
                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                  {{ $t("Sale.Close") }}
                </a>
              </div>
            </div>
          </div>
        </div>
      </div>
      <hr class="hr-dashed hr-menu mt-0" />
      <div class="row">
        <div class="col-lg-12">
          <div class="card border-0">
            <div class="card-header border-0">
              <div class="row">
                <div class="col-md-3 col-lg-3 col-12 form-group">
                  <label>{{ $t("Search By BarCode Type") }}</label>
                  <multiselect :options="['None', 'Generated', 'Scanned']" v-model="barCodeType" :key="counter"
                    :show-labels="false" placeholder="Select Type">
                  </multiselect>
                </div>
                <div class="col-md-3 col-lg-3 col-12 form-group">
                  <label>{{ $t("Product.SearchbyWareHosue") }}</label>
                  <warehouse-dropdown v-model="warehouseId" :key="counter" />
                </div>

                <div class="col-md-3 col-lg-3 col-12 form-group">
                  <label>{{ $t("Product.SearchbyProduct") }}</label>
                  <productMasterdropdown v-model="produtMaster" :key="counter">
                  </productMasterdropdown>
                </div>
                <div class="col-md-3 col-lg-3 col-12 form-group">
                  <label>{{ $t("Product.SearchbyCategory") }}</label>
                  <categorydropdown v-model="category" :key="counter"></categorydropdown>
                </div>
                <div class="col-md-3 col-lg-3 col-12 form-group">
                  <label>{{ $t("Product.SearchbySubCategory") }}</label>
                  <multiselect v-model="subCategory" :options="subCategoryOptions" :show-labels="false" :key="counter"
                    track-by="name" label="name" v-bind:class="$i18n.locale == 'en' ? 'text-left ' : 'arabicLanguage '
                      " v-bind:placeholder="$t('AddProduct.PleaseSelectSubCategory')
    ">
                  </multiselect>
                </div>
                <div class="col-md-3 col-lg-3 col-12 form-group">
                  <label>{{ $t("Product.SearchbyColor") }}</label>
                  <colordropdown v-model="color" :key="counter" />
                </div>
                <div class="col-md-3 col-lg-3 col-12 form-group">
                  <label>{{ $t("Product.SearchbySize") }}</label>
                  <sizedropdown v-model="size" :key="counter" />
                </div>
                <div class="col-xs-12">
                  <button v-on:click="SearchFilter(true)" type="button" class="btn btn-outline-primary mt-3">
                    {{ $t("Sale.ApplyFilter") }}
                  </button>
                  <button v-on:click="ClearFilter()" type="button" class="btn btn-outline-primary mx-2 mt-3">
                    {{ $t("Sale.ClearFilter") }}
                  </button>
                </div>
              </div>
            </div>
            <hr class="hr-dashed hr-menu mt-2" />
            <div class="card-body border-0">
              <p v-if="!deleteAllGeneratedCode">
                <b>Do you want to create barcode for all load products?</b>
                &nbsp;&nbsp;
                <button class="btn btn-outline-primary" v-on:click="generateBarcodeForAllProducts(true)">
                  {{ $t("Create") }}
                </button>
              </p>
              <p v-if="deleteAllGeneratedCode">
                <b>Do you want to delete all generated barcode for all load
                  products?</b>
                &nbsp;&nbsp;
                <button class="btn btn-outline-danger" v-on:click="generateBarcodeForAllProducts(false)">
                  {{ $t("Delete") }}
                </button>
              </p>
              <div class="table-responsive">
                <table class="table mb-0">
                  <thead class="thead-light table-hover">
                    <tr>
                      <th>#</th>
                      <th>{{ $t("Product.Code") }}</th>
                      <th>
                        {{ $t("Product.ItemName") }}
                      </th>
                      <th>
                        {{ $t("Style/Model Number") }}
                      </th>
                      <th>
                        {{ $t("Pack Size") }}
                      </th>
                      <th>
                        {{ $t("BarCode") }}
                      </th>
                      <th>
                        {{ $t("New BarCode") }}
                      </th>
                      <th></th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr v-for="(details, index) in productList" v-bind:key="details.id">
                      <td>
                        {{ index + 1 }}
                      </td>
                      <td>
                        {{ details.code }}
                      </td>
                      <td>
                        {{ details.displayName }}
                      </td>
                      <td>
                        {{ details.styleNumber }}
                      </td>
                      <td>
                        {{ details.unitPerPack }}
                      </td>
                      <td v-if="details.barCode != null || details.barCode == ''">
                        {{ details.barCode }}
                      </td>
                      <td v-else>---</td>
                      <td>
                        <input v-model="details.newBarCode" class="form-control" />
                      </td>
                      <td>
                        <button v-if="details.newBarCode == null ||
                          details.newBarCode == ''
                          " class="btn btn-outline-primary" v-on:click="generateBarcode(details.code, false)">
                          {{ $t("AddProduct.Generate") }}
                        </button>
                        <button v-if="details.newBarCode != '' &&
                          details.newBarCode != null
                          " class="btn btn-outline-danger" v-on:click="generateBarcode(details.code, true)">
                          {{ $t("AddProduct.Delete") }}
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
      <div class="col-lg-12 invoice-btn-fixed-bottom">
        <div class="col-md-12 arabicLanguage">
          <button class="btn btn-outline-primary me-2" v-on:click="SaveItemBarCodeSetup()">
            <i class="far fa-save"></i> {{ $t("Save") }}
          </button>

          <button class="btn btn-danger me-2" v-on:click="GotoPage('/StartScreen')">
            {{ $t("AddPurchase.Cancel") }}
          </button>
        </div>
      </div>
    </div>

    <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
  </div>
</template>
<script>
import clickMixin from "@/Mixins/clickMixin";
import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
import Multiselect from "vue-multiselect";

export default {
  mixins: [clickMixin],
  name: "ItemsBarCode",
  components: {
    Loading,
    Multiselect,
  },
  data: function () {
    return {
      loading: false,
      warehouseId: "",
      produtMaster: "",
      category: "",
      subCategory: "",
      color: "",
      size: "",
      barCodeType: "",
      subCategoryOptions: [],
      currentPage: 100,
      pageCount: "",
      rowCount: "",
      productList: [],
      counter: 1,
      deleteAllGeneratedCode: false,
      productFilter: {
        productMasterId: "",
        categoryId: "",
        subCategoryId: "",
        sizeId: "",
        colorId: "",
        originId: "",
        warehouseId: "",
        searchTerm: "",
        pageNumber: 1,
        isDropdown: false,
        branchId: "",
      },
    };
  },

  methods: {
    ImportDataFromCsv: function () {
        var root = this;
        root.$router.push({
            path: '/ImportExportRecords',
            query: {data:'Product'}
        })
    },
    DowmloadCSV: function () {
      var root = this;
      var token = "";
      root.loading = true;

      if (token == "") {
        token = localStorage.getItem("token");
      }

      var subCategoryId = "";
      if(this.subCategory == null)
      {
        subCategoryId = "";
      }
      else
      {
        if (this.subCategory != '') {
          subCategoryId = this.subCategory.id;
        }
      }
      if(this.warehouseId == null)
      {
        this.warehouseId = '';
      }
      if(this.produtMaster == null)
      {
        this.produtMaster = '';
      }
      if(this.subCategoryId == null)
      {
        this.subCategoryId = '';
      }
      if(this.category == null)
      {
        this.category = '';
      }
      if(this.color == null)
      {
        this.color = '';
      }
      if(this.size == null)
      {
        this.size = '';
      }
      if(this.barCodeType == null)
      {
        this.barCodeType = '';
      }

      var url =
        "/Product/GetProductListExcelForBarCodes?warehouseId=" +
        this.warehouseId +
        "&produtMasterId=" +
        this.produtMaster +
        "&categoryId=" +
        this.category +
        "&subCategoryId=" +
        subCategoryId +
        "&colorId=" +
        this.color +
        "&sizeId=" +
        this.size +
        "&barCodeType=" +
        this.barCodeType +
        "&pageNumber=" +
        this.currentPage;

      root.$https
        .post(url, this.productFilter, {
          headers: { Authorization: `Bearer ${token}` },
          responseType: "blob",
        })
        .then(function (response) {
          const url = window.URL.createObjectURL(new Blob([response.data]));
          const link = document.createElement("a");
          link.href = url;
          link.setAttribute("download", "Item Template.xlsx");
          document.body.appendChild(link);
          link.click();
          root.loading = false;
        })
        .catch((error) => {
          console.log(error);
          root.loading = false;
          root.$swal.fire({
            icon: "error",
            type: "error",
            title: root.$t("Error"),
            text: root.$t("Something went Wrong"),
            showConfirmButton: false,
            timer: 5000,
            timerProgressBar: true,
          });
        });
    },
    generateBarcodeForAllProducts: function (x) {
      this.loading = true;
      if (x) {
        this.productList.forEach((product) => {
          var randomNumber = Math.floor(Math.random() * 10000000000);
          product.newBarCode = randomNumber;
          product.isGenerated = true;
          this.deleteAllGeneratedCode = x;
        });
      } else {
        this.productList.forEach((product) => {
          product.newBarCode = "";
          product.isGenerated = false;
          this.deleteAllGeneratedCode = x;
        });
      }

      this.loading = false;
    },
    generateBarcode: function (code, x) {
      if (x) {
        var product1 = this.productList.find((x) => x.code == code);
        product1.newBarCode = "";
        product1.isGenerated = false;
      } else {
        var randomNumber = Math.floor(Math.random() * 10000000000);
        var product = this.productList.find((x) => x.code == code);
        product.newBarCode = randomNumber;
        product.isGenerated = true;
      }
    },
    GotoPage: function (link) {
      this.$router.push({ path: link });
    },
    SearchFilter: function (val) {
      
      if (val) {
        this.productList = [];
        this.currentPage = 100;
      }
      var subCategoryId = "";
      if(this.subCategory == null)
      {
        subCategoryId = "";
      }
      else
      {
        if (this.subCategory != '') {
          subCategoryId = this.subCategory.id;
        }
      }
      
      

      

      var root = this;
      this.loading = true;

      if(this.warehouseId == null)
      {
        this.warehouseId = '';
      }
      if(this.produtMaster == null)
      {
        this.produtMaster = '';
      }
      if(this.subCategoryId == null)
      {
        this.subCategoryId = '';
      }
      if(this.category == null)
      {
        this.category = '';
      }
      if(this.color == null)
      {
        this.color = '';
      }
      if(this.size == null)
      {
        this.size = '';
      }
      if(this.barCodeType == null)
      {
        this.barCodeType = '';
      }

      var url =
        "/Product/GetItemsForBarCodeChange?warehouseId=" +
        this.warehouseId +
        "&produtMasterId=" +
        this.produtMaster +
        "&categoryId=" +
        this.category +
        "&subCategoryId=" +
        subCategoryId +
        "&colorId=" +
        this.color +
        "&sizeId=" +
        this.size +
        "&barCodeType=" +
        this.barCodeType +
        "&pageNumber=" +
        this.currentPage;
      var token = "";
      if (token == "") {
        token = localStorage.getItem("token");
      }

      root.$https
        .post(url, this.productFilter, {
          headers: { Authorization: `Bearer ${token}` },
        })
        .then(function (response) {
          if (response.data != null) {
            root.productList = response.data.results.products;
            root.deleteAllGeneratedCode = false;
          }
          root.loading = false;
        });
    },
    ClearFilter: function () {
      this.barCodeType = "";
      this.warehouseId = "";
      this.produtMaster = "";
      this.category = "";
      this.subCategory = "";
      this.color = "";
      this.size = "";
      this.currentPage = 100;
      this.counter++;
      this.SearchFilter();
    },
    getSubcategory: function () {
      //this.catId = event;
      var root = this;
      var token = "";
      if (token == "") {
        token = localStorage.getItem("token");
      }

      this.subCategoryOptions = [];
      this.$https
        .get("/Product/GetSubCategoryInformation?isActive=true", {
          headers: { Authorization: `Bearer ${token}` },
        })
        .then(function (response) {
          if (response.data != null) {
            response.data.results.subCategories.forEach(function (rout) {
              root.subCategoryOptions.push({
                id: rout.id,
                name:
                  root.$i18n.locale == "en"
                    ? rout.name != ""
                      ? rout.code + " " + rout.name
                      : rout.code + " " + rout.nameArabic
                    : rout.nameArabic != ""
                      ? rout.code + " " + rout.nameArabic
                      : rout.code + " " + rout.name,
              });
            });
          }
        });
    },
    handleScrolling: async function () {
      if (
        window.scrollY + window.innerHeight >=
        document.body.scrollHeight - 50
      ) {
        var total = this.currentPage + 100;
        this.currentPage = total;
       await this.SearchFilter();
      }
    },
    SaveItemBarCodeSetup: function () {
      this.loading = true;
      var root = this;
      var token = "";
      if (token == "") {
        token = localStorage.getItem("token");
      }

      this.$https
        .post("/Product/SaveItemsBarCodeList", this.productList, {
          headers: { Authorization: `Bearer ${token}` },
        })
        .then(function (response) {
          if (response.data.isSuccess) {
            root.$swal({
              title:
                root.$i18n.locale == "en" || root.isLeftToRight()
                  ? "Saved!"
                  : "!تم الحفظ",
              text: response.data.isAddUpdate,
              type: "success",
              icon: "success",
              showConfirmButton: false,
              timer: 2000,
              timerProgressBar: true,
              confirmButtonClass: "btn btn-success",
              buttonsStyling: false,
            });
            root.ClearFilter();
          } else {
            root.$swal({
              title:
                root.$i18n.locale == "en" || root.isLeftToRight()
                  ? "Error!"
                  : "خطأ",
              text: response.data.isAddUpdate,
              type: "error",
              confirmButtonClass: "btn btn-info",
              buttonsStyling: false,
            });
          }
          root.loading = false;
        })
        .catch(() => {
          root.$swal({
            title:
              root.$i18n.locale == "en" || root.isLeftToRight()
                ? "Error!"
                : "خطأ",
            text: "Please Contact with Support.",
            type: "error",
            confirmButtonClass: "btn btn-info",
            buttonsStyling: false,
          });
          root.loading = false;
        });
    },
  },

  created: function () {
    this.getSubcategory();
  },
  mounted: function () {
    this.SearchFilter();
    window.addEventListener("scroll", this.handleScrolling);
  },
};
</script>
