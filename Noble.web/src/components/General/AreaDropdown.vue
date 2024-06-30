<template>
  <div>
    <multiselect
      v-model="DisplayValue"
      :options="options"
      :multiple="false"
      v-bind:placeholder="$t('AreaDropdown.SelectOption')"
      track-by="area"
      :clear-on-select="false"
      :show-labels="false"
      label="area"
      :preselect-first="true"
    ></multiselect>
  </div>
</template>
<script>
import Multiselect from "vue-multiselect";
import clickMixin from "@/Mixins/clickMixin";
    import { maxLength, requiredIf } from "@vuelidate/validators";
import useVuelidate from "@vuelidate/core";
export default {
  mixins: [clickMixin],

  name: "AreaDropdown",
  props: ["modelValue"],

  components: {
    Multiselect
  },
  setup() {
    return { v$: useVuelidate() };
  },
  data: function() {
    return {
      options: [],
      value: "",
      show: false,
      type: "",

      render: 0
    };
  },
  validations() {
    return {
      allowanceType: {
        name: {
          maxLength: maxLength(50)
        },
        nameArabic: {
          required: requiredIf(function () {
                            return !this.allowanceType.name;
                        }),
          // required: requiredIf(x => {
          //   if (x.name == "" || x.name == null) return true;
          //   return false;
          // }),
          maxLength: maxLength(50)
        },

        description: {
          maxLength: maxLength(200)
        }
      }
    };
  },
  methods: {
    EmptyRecord: function() {
      this.DisplayValue = "";
    },
    getData: function() {
      var root = this;
      var token = "";
      if (token == '') {
        token = localStorage.getItem("token");
      }
      this.$https
        .get("/Region/RegionList?isActive=true", {
          headers: { Authorization: `Bearer ${token}` }
        })
        .then(function(response) {
          if (response.data != null) {
            response.data.regions.forEach(function(result) {
              root.options.push({
                id: result.id,
                area: result.code + " " + result.area
              });
            });
          }
        })
        .then(function() {
          root.value = root.options.find(function(x) {
            return x.id == root.modelValue;
          });
        });
    }
  },

  computed: {
    DisplayValue: {
      get: function() {
        if (this.value != "" || this.value != undefined) {
          return this.value;
        }
        return this.modelValue;
      },
      set: function(value) {
        this.value = value;
        this.$emit("update:modelValue", value.id, value);
      }
    }
  },

  mounted: function() {
    this.getData();
  }
};
</script>