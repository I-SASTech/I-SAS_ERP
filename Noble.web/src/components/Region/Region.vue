<template>
    <div>
      <div class="page-header" style="text-align: center">
        I'm The Header
        <br/>
        <button type="button" @click="printPage" style="background: pink">
          PRINT ME!
        </button>
      </div>
  
      <div class="page-footer">
        I'm The Footer
      </div>
  
      <table>
        <thead>
          <tr>
            <td>
              <div class="page-header-space"></div>
            </td>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>
              <div class="page">PAGE 1</div>
              <div class="page">PAGE 2</div>
              <div class="page" style="line-height: 3;">
               Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc tincidunt metus eu consectetur rutrum. Praesent tempor facilisis dapibus. Aliquam cursus diam ac vehicula pulvinar. Integer lacinia non odio et condimentum. Aenean faucibus cursus mi, sed interdum turpis sagittis a. Quisque quis pellentesque mi. Ut erat eros, posuere sed scelerisque ut, pharetra vitae tellus. Suspendisse ligula sapien, laoreet ac hendrerit sit amet, viverra vel mi. Pellentesque faucibus nisl et dolor pharetra, vel mattis massa venenatis. Integer congue condimentum nisi, sed tincidunt velit tincidunt non. Nulla sagittis sed lorem pretium aliquam. Praesent consectetur volutpat nibh, quis pulvinar est volutpat id. Cras maximus odio posuere suscipit venenatis. Donec rhoncus scelerisque metus, in tempus erat rhoncus sed. Morbi massa sapien, porttitor id urna vel, volutpat blandit velit. Cras sit amet sem eros. Quisque commodo facilisis tristique. Proin pellentesque sodales rutrum. Vestibulum purus neque, congue vel dapibus in, venenatis ut felis. Donec et ligula enim. Sed sapien sapien, tincidunt vitae lectus quis, ultricies rhoncus mi. Nunc dapibus nulla tempus nunc interdum, sed facilisis ex pellentesque. Nunc vel lorem leo. Cras pharetra sodales metus. Cras lacus ex, consequat at consequat vel, laoreet ac dui. Curabitur aliquam, sapien quis congue feugiat, nisi nisl feugiat diam, sed vehicula velit nulla ac nisl. Aliquam quis nisi euismod massa blandit pharetra nec eget nunc. Etiam eros ante, auctor sit amet quam vel, fringilla faucibus leo. Morbi a pulvinar nulla. Praesent sed vulputate nisl. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Aenean commodo mollis iaculis. Maecenas consectetur enim vitae mollis venenatis. Ut scelerisque pretium orci id laoreet. In sit amet pharetra diam. Vestibulum in molestie lorem. Nunc gravida, eros non consequat fermentum, ex orci vestibulum orci, non accumsan sem velit ac lectus. Vivamus malesuada lacus nec velit dignissim, ac fermentum nulla pretium. Aenean mi nisi, convallis sed tempor in, porttitor eu libero. Praesent et molestie ante. Duis suscipit vitae purus si
              Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc tincidunt metus eu consectetur rutrum. Praesent tempor facilisis dapibus. Aliquam cursus diam ac vehicula pulvinar. Integer lacinia non odio et condimentum. Aenean faucibus cursus mi, sed interdum turpis sagittis a. Quisque quis pellentesque mi. Ut erat eros, posuere sed scelerisque ut, pharetra vitae tellus. Suspendisse ligula sapien, laoreet ac hendrerit sit amet, viverra vel mi. Pellentesque faucibus nisl et dolor pharetra, vel mattis massa venenatis. Integer congue condimentum nisi, sed tincidunt velit tincidunt non. Nulla sagittis sed lorem pretium aliquam. Praesent consectetur volutpat nibh, quis pulvinar est volutpat id. Cras maximus odio posuere suscipit venenatis. Donec rhoncus scelerisque metus, in tempus erat rhoncus sed. Morbi massa sapien, porttitor id urna vel, volutpat blandit velit. Cras sit amet sem eros. Quisque commodo facilisis tristique. Proin pellentesque sodales rutrum. Vestibulum purus neque, congue vel dapibus in, venenatis ut felis. Donec et ligula enim. Sed sapien sapien, tincidunt vitae lectus quis, ultricies rhoncus mi. Nunc dapibus nulla tempus nunc interdum, sed facilisis ex pellentesque. Nunc vel lorem leo. Cras pharetra sodales metus. Cras lacus ex, consequat at consequat vel, laoreet ac dui. Curabitur aliquam, sapien quis congue feugiat, nisi nisl feugiat diam, sed vehicula velit nulla ac nisl. Aliquam quis nisi euismod massa blandit pharetra nec eget nunc. Etiam eros ante, auctor sit amet quam vel, fringilla faucibus leo. Morbi a pulvinar nulla. Praesent sed vulputate nisl. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Aenean commodo mollis iaculis. Maecenas consectetur enim vitae mollis venenatis. Ut scelerisque pretium orci id laoreet. In sit amet pharetra diam. Vestibulum in molestie lorem. Nunc gravida, eros non consequat fermentum, ex orci vestibulum orci, non accumsan sem velit ac lectus. Vivamus malesuada lacus nec velit dignissim, ac fermentum nulla pretium. Aenean mi nisi, convallis sed tempor in, porttitor eu libero. Praesent et molestie ante. Duis suscipit vitae purus si
              </div>
            </td>
          </tr>
        </tbody>
        <tfoot>
          <tr>
            <td>
              <div class="page-footer-space"></div>
            </td>
          </tr>
        </tfoot>
      </table>
    </div>
  </template>
  
  <script>
  export default {
    name: 'YourComponentName',
    methods: {
      printPage() {
        window.print();
      }
    }
  }
  </script>
  
  <style>
  /* Adjusted CSS for increased header and footer height */
  .page-header, .page-header-space {
    height: 150px; /* Increased from 100px */
  }
  
  .page-footer, .page-footer-space {
    height: 100px; /* Increased from 50px */
  }
  
  .page-footer {
    position: fixed;
    bottom: 0;
    width: 100%;
    border-top: 1px solid black;
    background: yellow;
  }
  
  .page-header {
    position: fixed;
    top: 0mm;
    width: 100%;
    border-bottom: 1px solid black;
    background: yellow;
  }
  
  .page {
    page-break-after: always;
  }
  
  @page {
    margin: 20mm
  }
  
  @media print {
     thead {display: table-header-group;} 
     tfoot {display: table-footer-group;}
     button {display: none;}
     body {margin: 0;}
  }
  </style>