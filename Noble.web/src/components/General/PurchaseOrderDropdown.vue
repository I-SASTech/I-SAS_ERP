<template>
    <div>
        <multiselect v-model="DisplayValue"
                     :options="options"
                     :multiple="false"
                     track-by="name"
                     :clear-on-select="false"
                     :show-labels="false"
                     label="name"
                     :disabled="disable"
                     placeholder="Select Order"                  
                     :preselect-first="true">
        </multiselect>
    </div>
</template>
<script>
import Multiselect from "vue-multiselect";
import moment from "moment";

    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
  //name: "SupplierDropdown",
        props: ["modelValue",'disable','supplierQuotation','contactAccountId','isPartially'],

  components: {
    Multiselect,
  },
  data: function () {
    return {
      options: [],
      value: "",
    };
  },
  methods: {
    getDate: function (x) {
                return moment(x).format('DD MMM YYYY');
            },
    GetAmountOfSelected: function () {
                if (this.DisplayValue.length > 0)
                    return this.options[0].netAmount;
                else
                    return this.DisplayValue.netAmount;
            },

            Clear: function () {
              this.value='';
               
            },
    getData: function () {
      var root = this;
      var token = "";
      if (token == '') {
        token = localStorage.getItem("token");
      }
          var branchId = localStorage.getItem('BranchId');

          

          var isPartially=false;
                if(this.isPartially)
                {
                    isPartially=true;
                }

          var contactAccountId='';
                if(this.contactAccountId!=null && this.contactAccountId!=undefined && this.contactAccountId!='' )
                {
                    contactAccountId=this.contactAccountId;

                } 

                var status='Approved';

      var formName = this.supplierQuotation == undefined ? '' : this.supplierQuotation;
      this.$https
          .get('/Purchase/PurchaseOrderList?IsDropDown=' + true+ '&documentType=' + formName+ '&branchId=' + branchId+ '&contactAccountId=' + contactAccountId+ '&isPartially=' +isPartially + '&status=' +status, {
          headers: { Authorization: `Bearer ${token}` },
        })
          .then(function (response) {
              
          if (response.data != null) {
            response.data.results.forEach(function (sup) {
              root.options.push({
                id: sup.id,
                  name: sup.registrationNumber +  ' - ' + localStorage.getItem('currency') + sup.netAmount + " - " + root.getDate(sup.date),
                  netAmount: sup.netAmount,
              });
            });
          }
        })
        .then(function () {
            root.value = root.options.find(function (x) {
            return x.id == root.modelValue;
          });
        });
    },
  },
  computed: {
    DisplayValue: {
      get: function () {
        if (this.value != "" || this.value != undefined) {
          return this.value;
        }
        return this.modelValue;
      },
      set: function (value) {

        if(value != null)
        {
          this.value = value;
          this.$emit("update:modelValue", value.id);
        }
        else{
          this.value = value;
          this.$emit("update:modelValue", null);
        }
      },
    },
  },
  mounted: function () {
    this.getData();
  },
};
</script>